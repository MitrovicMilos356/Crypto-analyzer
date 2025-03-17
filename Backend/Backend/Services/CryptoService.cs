using Backend.API;
using Backend.DTOs;
using Backend.Models;
using System.Net;

namespace Backend.Services
{
    public class CryptoService
    {
        CryptoAPI cryptoAPI = new CryptoAPI();
        public List<CoinDto> GetCryptoData()
        {
            
            
            return cryptoAPI.Coins;
        }
        public List<CryptoHistoricalDto> GetCryptoHistory(string symbol,DateTime startDate, DateTime endDate)
        {
            

            return cryptoAPI.GetCryptoHistory(symbol, startDate, endDate);
        }
    }
   
}
