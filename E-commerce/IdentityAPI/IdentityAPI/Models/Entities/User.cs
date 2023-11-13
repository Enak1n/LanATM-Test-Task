namespace IdentityAPI.Domain.Entities
{
    public class User 
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }    
        public string Surname { get; set; }
        public Role Role { get; set; }
        public Guid? AddressId { get; set; }
        public DateTimeOffset CreatedAtDateUtc { get; set; }
    }
}
