using Microsoft.AspNetCore.Identity;

namespace IdentityAPI.DataBase.Entities
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public Guid? AddressId { get; set; }
        public Address? Address { get; set; }
        public DateTimeOffset RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}
