using Ecommerce.Dto;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailingController : ControllerBase
    {
        private readonly IMailingService _mailingService;
        public MailingController(IMailingService mailingService)
        {
            _mailingService = mailingService;
        }
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail([FromForm] MailDto mailDto)
        {
            await _mailingService.SendEmailAsync(mailDto.ToEmail , mailDto.Subject , mailDto.Body , mailDto.Attachments);
            return Ok();
        }
        [HttpPost("WelcomeEmail")]
        public async Task<IActionResult> SendWelcomeMail([FromBody] WelcomeMailDto mailDto)
        {
            var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\EmailTemplate.html";
            var str = new StreamReader(filePath);
            var mailText = str.ReadToEnd();
            str.Close();
            mailText = mailText.Replace("[username]", mailDto.UserName).Replace("[email]",mailDto.Email);
            await _mailingService.SendEmailAsync(mailDto.Email, "Welcome to our website ", mailText);
            return Ok();
        }
    }
}
