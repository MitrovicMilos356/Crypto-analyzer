using Backend.DTOs;
using Backend.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Backend.API
{
    public class CryptoAPI
    {
        private readonly GoldService _goldService = new GoldService();
        private static readonly string API_KEY = "7c65a964-411f-42c0-8207-c4a710d9df81";
        private readonly HttpClient _httpClient;

        public List<CoinDto> Coins { get; private set; } = new();

        public CryptoAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", API_KEY);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            Task.Run(async () => Coins = await MakeAPICall()).Wait(); // Fetch coins on startup
        }

        public async Task<List<CryptoHistoricalDto>> GetCryptoHistory(string symbol, DateTime startDate, DateTime endDate)
        {
            var url = new UriBuilder("https://min-api.cryptocompare.com/data/v2/histoday");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["fsym"] = symbol;
            queryString["tsym"] = "USD";
            queryString["limit"] = (endDate - startDate).Days.ToString();
            queryString["toTs"] = ((DateTimeOffset)endDate).ToUnixTimeSeconds().ToString();

            url.Query = queryString.ToString();

            try
            {
                string jsonResponse = await _httpClient.GetStringAsync(url.ToString());
                return ParseCryptoHistory(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching crypto history: {ex.Message}");
                return new List<CryptoHistoricalDto>();
            }
        }

        public async Task<List<CoinDto>> MakeAPICall()
        {
            var url = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "50";
            queryString["convert"] = "USD";

            url.Query = queryString.ToString();

            try
            {
                string jsonResponse = await _httpClient.GetStringAsync(url.ToString());
                return ParseCryptoData(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching crypto data: {ex.Message}");
                return new List<CoinDto>();
            }
        }

        private List<CryptoHistoricalDto> ParseCryptoHistory(string jsonResponse)
        {
            var historyList = new List<CryptoHistoricalDto>();

            try
            {
                var jsonData = JObject.Parse(jsonResponse);
                var historicalData = jsonData["Data"]["Data"];

                foreach (var dataPoint in historicalData)
                {
                    historyList.Add(new CryptoHistoricalDto
                    {
                        Date = DateTimeOffset.FromUnixTimeSeconds(dataPoint["time"].Value<long>()).UtcDateTime,
                        Open = dataPoint["open"].Value<decimal>(),
                        High = dataPoint["high"].Value<decimal>(),
                        Low = dataPoint["low"].Value<decimal>(),
                        Close = dataPoint["close"].Value<decimal>()
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing historical data: {ex.Message}");
            }

            return historyList;
        }

        private List<CoinDto> ParseCryptoData(string jsonResponse)
        {
            var coins = new List<CoinDto>();

            try
            {
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement root = doc.RootElement;
                JsonElement data = root.GetProperty("data");

                foreach (JsonElement coin in data.EnumerateArray())
                {
                    coins.Add(new CoinDto
                    {
                        Name = coin.GetProperty("name").GetString(),
                        Symbol = coin.GetProperty("symbol").GetString(),
                        Price = coin.GetProperty("quote").GetProperty("USD").GetProperty("price").GetDecimal()
                    });
                }
                CoinDto goldPrice = _goldService.GetGoldPrice();
                coins.Insert(0,goldPrice);

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing crypto data: {ex.Message}");
            }

            return coins;
        }
    }
}
