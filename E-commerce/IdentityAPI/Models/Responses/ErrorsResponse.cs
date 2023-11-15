using Microsoft.AspNetCore.Identity;

namespace IdentityAPI.Models.Responses
{
    public class ErrorsResponse : IResponse
    {
        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
