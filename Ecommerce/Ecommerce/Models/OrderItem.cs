using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int OrderId { get; set; }
        
       // public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
}
