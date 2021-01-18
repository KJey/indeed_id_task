using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using indeed_id.Models;
using indeed_id.Models.Operations;
using indeed_id.Services;

namespace indeed_id.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly ILogger<WalletController> _logger;
        private string _errMessage = "Неудачная операция";

        public WalletController(ILogger<WalletController> logger, IWalletService walletService)
        {
            _logger = logger;
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> Deposit([FromBody] Move model)
        {
            var result = await _walletService.Deposit(model.UserId, model.Currency, model.Amount);
            
            if (result) return Ok();
            
            return BadRequest(_errMessage);
        }
        [HttpPost]
        public async Task<IActionResult> Withdraw([FromBody] Move model)
        {
            var result = await _walletService.Withdraw(model.UserId, model.Currency, model.Amount);

            if (result) return Ok();

            return BadRequest(_errMessage);
        }
        [HttpPost]
        public async Task<IActionResult> Convert([FromBody] Convert model)
        {
            var result = await _walletService.Convert(model.UserId, model.FromCurrency, model.ToCurrency, model.Amount);

            if (result) return Ok();

            return BadRequest(_errMessage);
        }

        [HttpGet]
        public async Task<IActionResult> Info(int userId)
        {
            var result = await _walletService.Info(userId);

            if (result!=null) return Ok(result);

            return BadRequest(_errMessage);
        }
    }
}
