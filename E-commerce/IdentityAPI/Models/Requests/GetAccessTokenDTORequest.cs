using System.ComponentModel.DataAnnotations;

namespace IdentityAPI.Models.DTO.Requests;

public class GetAccessTokenDTORequest
{
    [Required]
    public string? RefreshToken { get; set; }
}
