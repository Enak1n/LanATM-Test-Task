namespace IdentityAPI.Models.Requests
{
    public class AddressDTORequest
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string? Apartment { get; set; }
    }
}
