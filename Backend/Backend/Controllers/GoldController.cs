using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/gold")]
    [ApiController]
    public class GoldController : ControllerBase
    {
        private readonly GoldService _goldService;

        public GoldController()
        {
            _goldService = new GoldService();
        }

        [HttpGet]
        public ActionResult<CoinDto> GetGoldPrice()
        {
            var goldPrice = _goldService.GetGoldPrice();
            return Ok(goldPrice);
        }
    }
}
