using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class AssignRoleDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
