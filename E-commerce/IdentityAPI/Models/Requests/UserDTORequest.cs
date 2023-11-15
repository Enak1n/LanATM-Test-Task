namespace IdentityAPI.Models.Requests
{
    public class UserDTORequest
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
