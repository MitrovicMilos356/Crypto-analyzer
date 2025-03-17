using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Backend.Controllers
{
    [Route("api/crypto")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CryptoService _cryptoService;
        public CryptoController()
        {
            _cryptoService = new CryptoService();
        }

        [HttpGet]
        public ActionResult<List<CoinDto>> GetCryptoData()
        {
            var coins = _cryptoService.GetCryptoData();
            return Ok(coins);

        }
        [HttpGet("history")]
        public IActionResult GetCryptoHistory(string symbol, string start, string end)
        {
            if (!DateTime.TryParse(start, out DateTime startDate) || !DateTime.TryParse(end, out DateTime endDate))
            {
                return BadRequest("Invalid date format. Use YYYY-MM-DD.");
            }

            List<CryptoHistoricalDto> history = _cryptoService.GetCryptoHistory(symbol, startDate, endDate);
            return Ok(history);
        }
    }
}
