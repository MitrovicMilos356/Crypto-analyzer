using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/crypto")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CryptoService _cryptoService;

        public CryptoController(CryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CoinDto>>> GetCryptoData()
        {
            var coins = await _cryptoService.GetCryptoData();
            return Ok(coins);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetCryptoHistory(string symbol, string start, string end)
        {
            if (!DateTime.TryParse(start, out DateTime startDate) || !DateTime.TryParse(end, out DateTime endDate))
            {
                return BadRequest("Invalid date format. Use YYYY-MM-DD.");
            }

            var history = await _cryptoService.GetCryptoHistory(symbol, startDate, endDate);
            return Ok(history);
        }
    }
}
