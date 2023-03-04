namespace Ecommerce.Dtos.OrderDtos
{
    public class OrderItemDto
    {
        public double Amount { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
