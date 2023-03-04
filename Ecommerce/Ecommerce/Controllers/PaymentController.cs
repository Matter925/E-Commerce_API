
using Ecommerce.Data;
using Ecommerce.Models.Payment;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Text;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
       
        private readonly IPayment _payment;
        public PaymentController(IPayment payment)
        {
            
            _payment = payment;
        }
        [Authorize]
        [HttpPost("Check-Credit/{CartId}")]
        public async Task<ActionResult> CheckCredit(int CartId)
        {
            var framesOfPayment = await _payment.CheckCredit(CartId);
            return Ok(framesOfPayment);

        }
        [HttpPost]
        [Route("payment-callback")]
        public async Task<IActionResult> PaymentCallback([FromBody] ResponsePayment data)
        {
            
            var result = await _payment.PaymentCallback(data);  
            
            return Ok(result);
        }
        

            

        //    return Ok(success);
        //}

    }
}
