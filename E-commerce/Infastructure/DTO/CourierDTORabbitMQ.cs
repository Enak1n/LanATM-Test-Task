namespace Infrastructure.DTO;

/// <summary>
/// Courier data transfer object used by RabbitMQ
/// </summary>
public class CourierDTORabbitMQ
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string PhoneNumber { get; set; }
}
