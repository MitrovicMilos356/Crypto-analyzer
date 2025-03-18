using Backend.API;
using Backend.DTOs;

namespace Backend.Services
{
    public class GoldService
    {
        private readonly GoldAPI _goldAPI = new GoldAPI();

        public CoinDto GetGoldPrice()
        {
            return _goldAPI.GoldPrice;
        }
    }
}
