using SupplyChainManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.ViewModels.Users
{
    public class SignUpRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
