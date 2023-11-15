using System.ComponentModel.DataAnnotations;

namespace IdentityAPI.Models.Requests
{
    public class LoginDTORequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
