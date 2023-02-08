using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dto.UserAuthDto
{
    public class CreatePasswordDto
    {
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string RestToken { get; set; }

    }
}
