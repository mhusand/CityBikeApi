namespace CityBikeApi.Services
{
    public class ApiPath   {
        //TODO: Next version could also add autodiscovery to https://gbfs.urbansharing.com/oslobysykkel.no/gbfs.json for better version handling.
        public static string StationInformation = $"station_information.json";
        public static string StationStatus = $"station_status.json";
    }
}
