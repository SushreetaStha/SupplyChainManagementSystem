using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SupplyChainManagement.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "User role is required")]
        public Role Role { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}