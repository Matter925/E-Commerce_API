using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Controllers
{
    public class RevokeToken
    {
        [Required]
        public string? Token { get; set; }
    }
}