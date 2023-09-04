using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using API.Entities;
using API.Data;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
        private readonly StoreContext _context;

        // Dependency Injection constructor
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

        // GET all action with filter parameters

        // GET by ID action
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
                var product = await _context.Products
                        .FindAsync(id);

                if (product == null) return NotFound();
                
                return product;
        }

        // POST action
        // Received by a form
        [HttpPost]
        public async Task<ActionResult<Product>> Post([Bind("Name", "Quantity")] Product product)
        {
                _context.Add(product);
                var result = await _context.SaveChangesAsync() > 0;

                if (result) return CreatedAtRoute("GetProduct", new { ID = product.ID }, product);

                return BadRequest(new ProblemDetails { Title = "Problem creating new product" });
        }

        // PUT action
        [HttpPut]
        public async Task<ActionResult<Product>> Put([FromForm] Product product)
        {
                _context.Products.Update(product);

                var result = await _context.SaveChangesAsync() > 0;

                if (result) return Ok(product);

                return BadRequest(new ProblemDetails { Title = "Problem updating product" });
        }

        // DELETE action
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
                var product = await _context.Products.FindAsync(id);

                if (product == null) return NotFound();

                _context.Products.Remove(product);

                var result = await _context.SaveChangesAsync() > 0;

                if (result) return Ok(product);

                return BadRequest(new ProblemDetails { Title = "Problem remove product" });
        }
}

// TODO: Authentication for POST, PUT, and DELETE
// TODO: Pagination for results
// TODO: DTO to Domain Entity mapping
// TODO: Add logging 
