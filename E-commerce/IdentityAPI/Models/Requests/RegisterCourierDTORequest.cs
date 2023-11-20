using System.ComponentModel.DataAnnotations;

namespace IdentityAPI.Models.DTO.Requests;

public class RegisterCourierDTORequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Incorrect Email")]
    public string? Email { get; set; }

    public DateTime? BirthDate { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
    [Required]
    public string? Name { get; set; }
}
