namespace OrdersApp.Models;
public class OrderDetailsModel
{
	public string Name { get; set; }
	public string Surname { get; set; }
	public uint Count { get; set; }
	public string PhoneNumber { get; set; }
	public string Address { get; set; }
	public string? Note { get; set; }
	public bool IsDelivered { get; set; } = false;
	public int ProductId { get; set; }

}