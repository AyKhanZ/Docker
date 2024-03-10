namespace OrdersApp.Models;

public class OrderDetails
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Surname { get; set; }
    public uint Count { get; set; }
    public string PhoneNumber { get; set; }

    public string Address { get; set; }
    public string? Note { get; set; }

    public bool IsDelivered { get; set; } = false;
    public Product Product { get; set; }
    public int ProductId { get; set; }

    public Order Order { get; set; }
    public int OrderId { get; set; }
}