using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using API.Entities;
using API.Data;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
        private readonly StoreContext _context;
        public ProductController(StoreContext context)
        {
                _context = context;
        }

        // GET all action
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
                // LINQ query on the EF context
                var products = await _context.Products
                        .ToListAsync();

                if (products == null) return NotFound();
                
                return products;
        }

        // GET by Id action

        // POST action
        // TODO: [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Product>> Create([Bind("Name", "Quantity")] Product product)
        {
                _context.Add(product);
                var result = await _context.SaveChangesAsync() > 0;

                if (result) return CreatedAtRoute("GetProduct", new { ID = product.ID }, product);

                return BadRequest(new ProblemDetails { Title = "Problem creating new product" });
        }

        // PUT action

        // DELETE action
}
