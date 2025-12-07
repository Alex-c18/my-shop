
using Microsoft.AspNetCore.Mvc;
using my_shop.Models;

namespace my_shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        public static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name="Laptop",Price=1000 },
            new Product { Id = 2,Name="Monitor",Price=200 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(_products);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            product.Id = _products.Count == 0 ? 1 : _products.Max(t => t.Id) + 1;
            _products.Add(product);
            return CreatedAtAction(nameof(GetProductById),new {Id=product.Id}, product);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Product updatedProduct)
        {
            var existingProduct = _products.FirstOrDefault(x => x.Id == id);
            if (existingProduct == null) return NotFound();

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;

            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(t => t.Id == id);
            if (product == null) return NotFound();

            _products.Remove(product);
            return NoContent();
        }
         

            

    }   
}
