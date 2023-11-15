namespace Infrastructure.DTO;

/// <summary>
/// Cart data transfer object used by RabbitMQ
/// </summary>
public class CartDTORabbitMQ
{
    public Guid Id { get; set; }
    public CartDTORabbitMQ(Guid id)
    {
        Id = id;
    }
}
