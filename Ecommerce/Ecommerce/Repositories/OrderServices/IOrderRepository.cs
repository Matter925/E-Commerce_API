

using Ecommerce.Dto.ReturnDto;
using Ecommerce.Dtos;
using Ecommerce.Models;

namespace Ecommerce.Repositories.OrderServices
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<Order>> GetOrderByUserId( string UserId);
        Task<Order> GetByPaymentId(int PaymentOrderId);
        Task<GeneralRetDto> CreateOrder(OrderDto Dto);
        Task<GeneralRetDto> UpdateOrderStatus(int PaymentOrderId);
        Task<GeneralRetDto> DeleteOrder(int OrderId);
    }
}
