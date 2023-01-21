using Ecommerce.Models.Payment;

namespace Ecommerce.Services
{
    public interface IPayment
    {
        public  Task<IFramesOfPayment> CheckCredit(double TotalAmount);
    }
}
