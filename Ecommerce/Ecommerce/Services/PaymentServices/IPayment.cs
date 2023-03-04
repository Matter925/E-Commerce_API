using Ecommerce.Dto.ReturnDto;
using Ecommerce.Models.Payment;

namespace Ecommerce.Services
{
    public interface IPayment
    {
        public  Task<IFramesOfPayment> CheckCredit(int CartId);
        public Task<GeneralRetDto> PaymentCallback(ResponsePayment data);
    }
}
