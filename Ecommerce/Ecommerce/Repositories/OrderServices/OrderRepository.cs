
using Ecommerce.Data;
using Ecommerce.Dto.ReturnDto;
using Ecommerce.Dtos;
using Ecommerce.Models;
using Ecommerce.Repositories.OrderItemsServices;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.OrderServices
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderItemsRepository _orderItemsRepository;
        public OrderRepository(ApplicationDbContext context , IOrderItemsRepository orderItemsRepository)
        {
            _context = context;
            _orderItemsRepository = orderItemsRepository;
        }
        public async Task<GeneralRetDto> CreateOrder(OrderDto Dto)
        {
            var cartItem = await _context.CartItems.Where(c=>c.CartId == Dto.CartId).ToListAsync();
            if(cartItem == null)
            {
                return new GeneralRetDto
                {
                    Message ="Cart is empty",
                    Success = false,
                };
            }
            var order = new Order
            {
                UserId = Dto.UserId,
                TotalAmount = Dto.TotalAmount,
                OrderDate = Dto.OrderDate,
                CartId = Dto.CartId,
                PaymentOrderId = Dto.PaymentOrderId,

            };
            await _context.AddAsync(order);
             _context.SaveChanges();

           await _orderItemsRepository.CreateOrderItems(Dto.PaymentOrderId);

            return new GeneralRetDto
            {
                Message ="success",
                Success = true,
            };
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders.Include(c => c.OrderItems).ThenInclude(d => d.Product).ToListAsync();
        }

        public Task<Order> GetByPaymentId(int PaymentOrderId)
        {
            return _context.Orders.SingleOrDefaultAsync(o=> o.PaymentOrderId == PaymentOrderId);
        }

        public async Task<IEnumerable<Order>> GetOrderByUserId(string UserId)
        {
            var orders = await _context.Orders.Include(c=>c.OrderItems).ThenInclude(d=>d.Product).Where(o => o.UserId == UserId).ToListAsync();    
            return orders;
        }

        public async Task<GeneralRetDto> UpdateOrderStatus(int PaymentOrderId)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(c=> c.PaymentOrderId == PaymentOrderId);
            if(order == null)
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = "",
                };
            }
            order.OrderStatus = true;
            _context.Orders.Update(order);
            _context.SaveChanges(true);
            return new GeneralRetDto
            {
                Success = true,
                Message = "Successfully Updated",
            };
        }
        public async Task<GeneralRetDto> DeleteOrder(int OrderId)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(c => c.Id == OrderId);
            if (order == null)
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = "Order Not Found",
                };
            }
            
            _context.Orders.Remove(order);
            _context.SaveChanges(true);
            return new GeneralRetDto
            {
                Success = true,
                Message = "Successfully Deleted",
            };
        }
    }
}
