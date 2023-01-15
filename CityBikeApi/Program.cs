using CityBikeApi.Services;
using CityBikeApi;
using CityBikeApi.ErrorHandler;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICityBikeService, CityBikeService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>c.EnableAnnotations());
var baseUrl = builder.Configuration.GetSection("CityBikeApi:BaseUrl").Value;
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });

var app = builder.Build();

app.UseMiddleware<ErrorHandler>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
