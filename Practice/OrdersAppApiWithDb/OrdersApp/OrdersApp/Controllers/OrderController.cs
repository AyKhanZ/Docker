using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersApp.DbContexts;
using OrdersApp.Models;

namespace OrdersApp.Controllers;
[Route("Order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrdersAykhanContext _dbContext;
    public OrderController(OrdersAykhanContext dbContext)
    {
        _dbContext = dbContext;
    }  

    [HttpGet("")]
    public async Task<IActionResult> GetOrdersWithOrderDetails()
    {
        var orders = await _dbContext.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.Images).ToListAsync();

        if (orders == null) return NotFound("Orders not found !!!"); 

        return Ok(orders);
    }
    [HttpPost("AddNewOrder")]
    public async Task<IActionResult> AddNewOrder([FromBody] OrderDetailsModel newOrderDetail)
    {
        var order = new Order { OrderDetails = new List<OrderDetails>() };

        var orderDetail = new OrderDetails
        {
            Name = newOrderDetail.Name,
            Surname = newOrderDetail.Surname,
            Address = newOrderDetail.Address,
            PhoneNumber = newOrderDetail.PhoneNumber,
            Count = newOrderDetail.Count,
            ProductId = newOrderDetail.ProductId,
            OrderId = order.Id
        };

        order.OrderDetails.Add(orderDetail);
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        return Ok(order);
    }

    [HttpDelete("DeleteOrder/{orderId}")]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        var order = await _dbContext.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null) return NotFound($"Order with id {orderId} not found!!!");

        _dbContext.OrderDetails.RemoveRange(order.OrderDetails);
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();

        return Ok($"Order with id {orderId} has been deleted!!!");
    }

    [HttpPost("ChangeDeliveryStatus/{orderDetailId}")]
    public async Task<IActionResult> ChangeDeliveryStatus(int orderDetailId, [FromBody] bool checkedStatus)
    {
        var orderDetail = await _dbContext.OrderDetails.FindAsync(orderDetailId);

        if (orderDetail == null) return NotFound($"OrderDetail with id {orderDetailId} not found!!!");

        orderDetail.IsDelivered = checkedStatus;

        var order = await _dbContext.Orders.Include(o => o.OrderDetails)
                                           .FirstOrDefaultAsync(o => o.OrderDetails.Any(od => od.Id == orderDetailId));
        if (order != null)
        {
            var orderDetailToUpdate = order.OrderDetails.FirstOrDefault(od => od.Id == orderDetailId);
            if (orderDetailToUpdate != null) orderDetailToUpdate.IsDelivered = checkedStatus;
            await _dbContext.SaveChangesAsync();
        }

        return Ok(order);
    }  
}