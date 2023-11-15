using IdentityAPI.DataBase;
using IdentityAPI.DataBase.Entities;
using IdentityAPI.Models.Requests;
using IdentityAPI.Models.Responses;

namespace IdentityAPI.Services
{
    public interface IUserService
    {
        public ICustomUserStore Store { get; }
        Task<User> GetById(Guid id);
        Task<LoginDTOResponse> GetAccessToken(string refreshToken);
        Task<IResponse> Register(User user, string Password, Role role);
        Task<LoginDTOResponse> Login(LoginDTORequest model);
        Task Logout(Guid userId);
        Task<bool> TokenIsActive(string token);
    }
}
