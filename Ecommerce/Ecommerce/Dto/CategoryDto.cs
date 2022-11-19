using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dto
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
