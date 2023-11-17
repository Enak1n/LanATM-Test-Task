namespace OrderAPI.Domain.Exceptions
{
    public class UniqueException : Exception
    {
        public UniqueException(string message) : base(message) { }
    }
}
