using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class ApplicationUser :IdentityUser
    {
        [Required ,MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }

        public Cart Cart { get; set; }

        public Favorite Favorite { get; set; }
        
    }
}
