using Backend.DTOs;
using Backend.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class CryptoService
    {
        private readonly CryptoAPI _cryptoAPI;

        public CryptoService(CryptoAPI cryptoAPI)
        {
            _cryptoAPI = cryptoAPI;
        }

        public async Task<List<CoinDto>> GetCryptoData()
        {
            return await _cryptoAPI.MakeAPICall();
        }

        public async Task<List<CryptoHistoricalDto>> GetCryptoHistory(string symbol, DateTime startDate, DateTime endDate)
        {
            return await _cryptoAPI.GetCryptoHistory(symbol, startDate, endDate);
        }
    }
}
