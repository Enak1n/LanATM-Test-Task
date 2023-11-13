using IdentityAPI.Models.Requests;
using IdentityAPI.Service.Models.Requests;
using IdentityAPI.Service.Models.Responses;

namespace IdentityAPI.Service.Auth.Interfaces
{
    public interface IAuthService
    {
        Task Register(UserRegistration user);
        Task<AuthenticateResponse> Login(string login, string password);
    }
}
