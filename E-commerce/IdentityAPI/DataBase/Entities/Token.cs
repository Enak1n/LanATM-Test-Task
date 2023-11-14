namespace IdentityAPI.DataBase.Entities
{
    public class Token
    {
        public Guid Id { get; set; }
        public Guid UserId { get; init; }
        public TokenType TokenType { get; init; }
        public string Value { get; init; }
        public bool IsActive { get; set; } = true;
    }
}
