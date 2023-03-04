using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dto
{
    public class ProductDto
    {
        [Required, MinLength(2, ErrorMessage = "Please enter at least  characters !")]
        public string Name { get; set; }
        [Required, MinLength(2, ErrorMessage = "Please enter at least 2 characters !")]
        public string Description { get; set; }
        [Required, MinLength(2, ErrorMessage = "Please enter at least 2 characters !")]
        public string ImageURL { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        
       
    }
}
