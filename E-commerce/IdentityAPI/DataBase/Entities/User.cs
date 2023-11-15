using Microsoft.AspNetCore.Identity;

namespace IdentityAPI.DataBase.Entities
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTimeOffset RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}
