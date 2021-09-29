using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CatalogAPI.Entities;
using CatalogAPI.Repositories.Interfaces;

namespace CatalogAPI.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductsRepository productsRepository, ILogger<CatalogController> logger)
        {
            _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync()
        {
            try
            {
                IEnumerable<Product> productsResult = await _productsRepository.GetProducts();
                return Ok(productsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Content(ex.Message, "Error");
            }
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(string productId)
        {
            try
            {
                //todo: I have to read about ASP.Net core validations
                if (string.IsNullOrWhiteSpace(productId)) return Content("Your id is empty.");

                Product productResult = await _productsRepository.GetProduct(productId);
                if (productResult is null)
                {
                    _logger.LogError($"Product with id: {productId} not found.");
                    return NotFound();
                }
                return Ok(productResult);
            }
            catch (Exception ex)
            {
                return Content(ex.Message, "Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryAsync(string categoryName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoryName)) return Content("Your category name is empty.");
                IEnumerable<Product> productsResult = await _productsRepository.GetProductByCategory(categoryName);
                if (productsResult is null)
                {
                    _logger.LogError($"Product with CategoryName: '{categoryName}' not found.");
                    return NotFound();
                }
                return Ok(productsResult);
            }
            catch (Exception ex)
            {
                return Content(ex.Message, "Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]Product product)
        {
            try
            {
                await _productsRepository.CreateProduct(product);
                return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return Content(ex.Message, "Error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                bool isUpdated = await _productsRepository.UpdateProduct(product);
                if (!isUpdated)
                {
                    _logger.LogError($"Failed to update product with id : '{product.Id}'");
                    return Content($"Failed to update product with id : '{product.Id}'");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(ex.Message, "Error");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            try
            {
                bool isDeleted = await _productsRepository.DeleteProduct(productId);
                if (!isDeleted)
                {
                    _logger.LogError($"Failed to delete product with id : '{productId}'");
                    return Content($"Failed to delete product with id : '{productId}'");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(ex.Message, "Error");
            }
        }
    }
}
