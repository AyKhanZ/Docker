namespace OrdersApp.Models;

public class Order
{
    public int Id { get; set; }
    public List<OrderDetails> OrderDetails { get; set; }

}