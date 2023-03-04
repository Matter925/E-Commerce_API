using Ecommerce.Dto.ReturnDto;

namespace Ecommerce.Services
{
    public interface IMailingService
    {
        Task <GeneralRetDto> SendEmailAsync (string mailTo , string subject ,string body , IList<IFormFile>attachments = null);
    }
}
