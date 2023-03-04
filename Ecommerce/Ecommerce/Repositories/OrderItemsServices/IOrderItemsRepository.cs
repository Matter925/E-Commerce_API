
using Ecommerce.Dto.ReturnDto;
using Ecommerce.Models;

namespace Ecommerce.Repositories.OrderItemsServices
{
    public interface IOrderItemsRepository
    {
        Task<GeneralRetDto> CreateOrderItems(int PaymentOrderId);
        Task<IEnumerable<OrderItem>> GetItemsByOrderID(int OrderId);
    }
}
