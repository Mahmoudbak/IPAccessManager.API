
using IPAccessManager.Core.Services.Content;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IPAccessManager.Services.GeoLocationService
{
    public class GeoLocationService : IGeoLocationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GeoLocationService(HttpClient httpClient, IConfiguration configuration)//IConfiguration
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<string?> GetCountryCodeFromIpAsync(string ipAddress)
        {

            var apiKey = _configuration["GeoLocationApi:ApiKey"];
            var baseUrl = _configuration["GeoLocationApi:BaseUrl"];


            var response = await _httpClient.GetAsync($"{baseUrl}?apiKey={apiKey}&ip={ipAddress}");

            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            using var jsonDocument = JsonDocument.Parse(content);

            if (jsonDocument.RootElement.TryGetProperty("country_code2", out var countryCode))
            {
                return countryCode.GetString();
            }

            return null;


        }
    }
}
