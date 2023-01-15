using CityBikeApi.Controllers;
using CityBikeApi.ErrorHandler;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace UnitTest
{
    [TestClass]
    public class CityBikeTests
    {
        public MockHttpMessageHandler? mockHttp;
        public HttpClient? client;
        public CityBikeService? service;

        [TestInitialize]
        public void TestInitialisering()
        {
            mockHttp = new MockHttpMessageHandler();
            client = new HttpClient(mockHttp);
            service = new CityBikeService(client);
            client.BaseAddress = new Uri("https://gbfs.urbansharing.com/oslobysykkel.no/");
        }

        [TestMethod]
        public void GetStationStatusReturnsAListWithTwoStations()
        {
            mockHttp.When($"https://gbfs.urbansharing.com/oslobysykkel.no/{ApiPath.StationInformation}")
                    .Respond("application/json", "{\r\n    \"last_updated\": 1673777945,\r\n    \"ttl\": 10,\r\n    \"version\": \"2.2\",\r\n    \"data\": {\r\n        \"stations\": [\r\n            {\r\n                \"station_id\": \"2355\",\r\n                \"name\": \"\\u00d8kern T-bane\",\r\n                \"address\": \"\\u00d8kernveien 147\",\r\n                \"rental_uris\": {\r\n                    \"android\": \"oslobysykkel://stations/2355\",\r\n                    \"ios\": \"oslobysykkel://stations/2355\"\r\n                },\r\n                \"lat\": 59.928894918817605,\r\n                \"lon\": 10.806234776281599,\r\n                \"capacity\": 12\r\n            },\r\n            {\r\n                \"station_id\": \"2350\",\r\n                \"name\": \"Blindern T-Bane\",\r\n                \"address\": \"Apalveien 60\",\r\n                \"rental_uris\": {\r\n                    \"android\": \"oslobysykkel://stations/2350\",\r\n                    \"ios\": \"oslobysykkel://stations/2350\"\r\n                },\r\n                \"lat\": 59.94022899411701,\r\n                \"lon\": 10.716856460117071,\r\n                \"capacity\": 25\r\n            }\r\n        ]\r\n    }\r\n}"); // Respond with JSON
            mockHttp.When($"https://gbfs.urbansharing.com/oslobysykkel.no/{ApiPath.StationStatus}")
                   .Respond("application/json", "{\r\n    \"last_updated\": 1673778166,\r\n    \"ttl\": 10,\r\n    \"version\": \"2.2\",\r\n    \"data\": {\r\n        \"stations\": [\r\n            {\r\n                \"station_id\": \"2355\",\r\n                \"is_installed\": 1,\r\n                \"is_renting\": 1,\r\n                \"is_returning\": 1,\r\n                \"last_reported\": 1673778166,\r\n                \"num_bikes_available\": 0,\r\n                \"num_docks_available\": 12\r\n            },\r\n            {\r\n                \"station_id\": \"2350\",\r\n                \"is_installed\": 1,\r\n                \"is_renting\": 1,\r\n                \"is_returning\": 1,\r\n                \"last_reported\": 1673778166,\r\n                \"num_bikes_available\": 5,\r\n                \"num_docks_available\": 20\r\n            }\r\n        ]\r\n    }\r\n}");
            var result = service?.GetListStationStatus().Result;

            Assert.AreEqual(result?.Count(), 2);
            Assert.IsTrue(result?.Any(x => x.station_id == 2355 && x.num_bikes_available == 0 && x.num_docks_available == 12 && x.name == "\u00d8kern T-bane"));
            Assert.IsTrue(result?.Any(x => x.station_id == 2350 && x.num_bikes_available == 5 && x.num_docks_available == 20 && x.name == "Blindern T-Bane"));
        }
        [TestMethod]
        public void WrongUriWillCause404()
        {
            mockHttp = new MockHttpMessageHandler();
            client = new HttpClient(mockHttp);
            client.BaseAddress = new Uri("https://wrongurl.com");
            var bikeService = new CityBikeService(client);
            var errorhandler = new Mock<ErrorHandler>();
            var controller = new CityBikeController(bikeService);
            var ex = Assert.ThrowsException<AggregateException>(() => controller.GetListStationStatus().Result);
            Assert.IsNotNull(ex);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual(((CityBikeApiException)ex.InnerException).StatusCode, System.Net.HttpStatusCode.NotFound);

        }
    }
}