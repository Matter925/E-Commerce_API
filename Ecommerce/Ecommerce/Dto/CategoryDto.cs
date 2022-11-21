using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dto
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageURL { get; set; }
    }
}
