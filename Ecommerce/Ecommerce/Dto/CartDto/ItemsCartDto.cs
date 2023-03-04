using Ecommerce.Models;

namespace Ecommerce.Dto.CartDto
{
    public class ItemsCartDto
    {
        public IEnumerable<CartItem> Items { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }   
    }
}
