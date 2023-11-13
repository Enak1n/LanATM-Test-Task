using System.ComponentModel.DataAnnotations;

namespace IdentityAPI.Service.Models.Requests
{
    public class UserRegistration
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$", ErrorMessage = "At least 8 characters, 1 uppercase letter, 1 number and 1 symbol")]
        [Required]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        public string Login { get; set; }
    }
}
