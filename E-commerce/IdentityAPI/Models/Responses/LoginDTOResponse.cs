namespace IdentityAPI.Models.Responses
{
    public class LoginDTOResponse : IResponse
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public LoginDTOResponse(
                                string accessToken,
                                string refreshToken,
                                Guid userId)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            UserId = userId;
        }
    }
}
