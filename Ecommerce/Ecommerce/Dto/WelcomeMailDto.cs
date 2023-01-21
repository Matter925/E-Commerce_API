using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dto
{
    public class WelcomeMailDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
