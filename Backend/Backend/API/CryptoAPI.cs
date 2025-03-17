using Backend.DTOs;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Web;
namespace Backend.API
{
    public class CryptoAPI
    {
        private static string API_KEY = "7c65a964-411f-42c0-8207-c4a710d9df81";

        public List<CoinDto> Coins { get; set; }

        public CryptoAPI()
        {
            try
            {
                Coins = makeAPICall();
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
                Coins = new List<CoinDto>(); // Return empty list if API call fails
            }
        }

        public List<CryptoHistoricalDto> GetCryptoHistory(string symbol, DateTime startDate, DateTime endDate)
        {
            var URL = new UriBuilder("https://min-api.cryptocompare.com/data/v2/histoday");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["fsym"] = symbol;  // Coin symbol (e.g., BTC)
            queryString["tsym"] = "USD";   // Convert to USD
            queryString["limit"] = (endDate - startDate).Days.ToString(); // Number of days
            queryString["toTs"] = ((DateTimeOffset)endDate).ToUnixTimeSeconds().ToString(); // End date timestamp

            URL.Query = queryString.ToString();

            using (var client = new WebClient())
            {
                client.Headers.Add("Accepts", "application/json");
                string jsonResponse = client.DownloadString(URL.ToString());

                var jsonData = JObject.Parse(jsonResponse);
                var historicalData = jsonData["Data"]["Data"];

                List<CryptoHistoricalDto> historyList = new List<CryptoHistoricalDto>();

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

                return historyList;
            }
        }
        public List<CoinDto> makeAPICall()
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "50";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");

            string jsonResponse = client.DownloadString(URL.ToString());

            return ParseCryptoData(jsonResponse);
        }

        private List<CoinDto> ParseCryptoData(string jsonResponse)
        {
            List<CoinDto> coins = new List<CoinDto>();

            using JsonDocument doc = JsonDocument.Parse(jsonResponse);
            JsonElement root = doc.RootElement;
            JsonElement data = root.GetProperty("data"); // Extract "data" array

            foreach (JsonElement coin in data.EnumerateArray())
            {
                coins.Add(new CoinDto
                {
                    Name = coin.GetProperty("name").GetString(),
                    Symbol = coin.GetProperty("symbol").GetString(),
                    Price = coin.GetProperty("quote").GetProperty("USD").GetProperty("price").GetDecimal()
                });
            }

            return coins;
        }

    }
}
