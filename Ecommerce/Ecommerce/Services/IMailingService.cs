namespace Ecommerce.Services
{
    public interface IMailingService
    {
        Task SendEmailAsync (string mailTo , string subject ,string body , IList<IFormFile>attachments = null);
    }
}
