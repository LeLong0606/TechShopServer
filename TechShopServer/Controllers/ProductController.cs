using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using TechShopServer.Data;
using TechShopServer.Models;
using TechShopServer.Services;

namespace TechShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IManageImage _iManageImage;

        public ProductController(ApiDbContext context, IWebHostEnvironment environment, IManageImage iManageImage)
        {
            _environment = environment;
            _context = context;
            _iManageImage = iManageImage;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProduct()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            _context.Add(product);

            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPost]
        [Route("uploadfile")]
        public async Task<IActionResult> UploadFile(IFormFile _IFromFile)
        {
            var result = await _iManageImage.UploadFile(_IFromFile);
            return Ok(result);
        }

        [HttpGet]
        [Route("dowloadfile")]
        public async Task<IActionResult> DowloadFile(string FileName)
        {
            var result = await _iManageImage.DowloadFile(FileName);
            return File(result.Item1, result.Item2, result.Item3);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update (int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Id correct product id");
            }
            
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();
        }



    }
}
