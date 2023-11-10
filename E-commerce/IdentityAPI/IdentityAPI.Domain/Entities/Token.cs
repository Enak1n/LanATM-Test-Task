namespace IdentityAPI.Domain.Entities
{
    public class Token
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public TokenType TokenType { get; set; }
        public bool IsExpired { get; set; } = false;
        public string TokenValue { get; set; }
    }
}
