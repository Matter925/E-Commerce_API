using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dto.UserAuthDto
{
    public class VerifyCodeDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
