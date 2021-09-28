using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CatalogAPI.Repositories.Interfaces;
using CatalogAPI.Entities;

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
    }
}
