using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(100)]
         public string Name { get; set; }
        
    }
}
