

using Ecommerce.Models;
using Ecommerce.Models.Payment;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckPaymentController : ControllerBase
    {
        private readonly IPayment _payment;

        public CheckPaymentController(IPayment payment)
        {
            _payment = payment;
        }

        [HttpPost("ChechCredit/{TotalAmount}")]
        public async Task<ActionResult> CheckCredit(double TotalAmount)
        {
            if(TotalAmount <= 0)
            {
                return BadRequest("Total Amount must be greater than 0 !! ");    
            }
            var framesOfPayment = await _payment.CheckCredit(TotalAmount);
            return Ok(framesOfPayment);  


        }

    }
}
