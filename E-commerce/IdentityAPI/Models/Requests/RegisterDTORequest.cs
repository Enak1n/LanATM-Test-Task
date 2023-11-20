using System.ComponentModel.DataAnnotations;

namespace IdentityAPI.Models.Requests
{
    public class RegisterDTORequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Incorrect Email")]
        public string? Email { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Surname { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$",
         ErrorMessage = "At least 8 characters, 1 uppercase letter, 1 number and 1 symbol")]
        public string? Password { get; set; }

        [Required]
        [Range(0, 2, ErrorMessage = "Incorrect role")]
        public Role Role { get; set; }
    }
}
