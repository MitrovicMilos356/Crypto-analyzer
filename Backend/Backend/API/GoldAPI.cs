using Backend.DTOs;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text.Json;
using System.Web;

namespace Backend.API
{
    public class GoldAPI
    {
        private static string API_URL = "https://api.gold-api.com/price/XAU";

        public CoinDto GoldPrice { get; set; }

        public GoldAPI()
        {
            try
            {
                GoldPrice = FetchGoldPrice();
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
                GoldPrice = new CoinDto{Name="Gold", Symbol = "XAU", Price = 0 }; // Default value if API call fails
            }
        }

        public CoinDto FetchGoldPrice()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Accepts", "application/json");

                string jsonResponse = client.DownloadString(API_URL);
                return ParseGoldData(jsonResponse);
            }
        }

        private CoinDto ParseGoldData(string jsonResponse)
        {
 
            using JsonDocument doc = JsonDocument.Parse(jsonResponse);
            JsonElement root = doc.RootElement;

            CoinDto coin= new CoinDto
            {
                Name = root.GetProperty("name").GetString(),
                Symbol = root.GetProperty("symbol").GetString(),
                Price = root.GetProperty("price").GetDecimal()
            };

            return coin;
        }
    }
}
