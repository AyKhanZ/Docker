using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersApp.DbContexts;

namespace OrdersApp.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly OrdersAykhanContext _dbContext;
        public ProductController(OrdersAykhanContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetProducts()
        {

            var products = await _dbContext.Products.Include(p => p.Images).ToListAsync();
            if (products == null)
            {
                return NotFound("Products not found");
            }

            return Ok(products);
        }


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProduct(int id)
        //{
        //    var product = await _dbContext.Products
        //        .Include(p => p.Images)
        //        .FirstOrDefaultAsync(p => p.Id == id);

        //    if (product == null)
        //    {
        //        return NotFound("Product not found");
        //    }

        //    return Ok(product);
        //}
    }
}
