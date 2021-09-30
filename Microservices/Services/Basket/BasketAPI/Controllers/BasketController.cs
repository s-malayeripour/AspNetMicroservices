using BasketAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using BasketAPI.Repositories.Interfaces;

namespace BasketAPI.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketRepository basketRepository, ILogger<BasketController> logger)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCart?>> TryGetBasketAsync(string userName)
        {
            try
            {
                ShoppingCart? shoppingCart = await _basketRepository.GetBasket(userName);
                return Ok(shoppingCart ?? new ShoppingCart(userName));//If it's null it's his first item .
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Content(ex.Message, "text/html");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart?>> TryUpdateBasketAsync([FromBody] ShoppingCart shoppingCart)
        {
            try
            {
                ShoppingCart? updatedShoppingCart = await _basketRepository.UpdateBasket(shoppingCart);
                return Ok(updatedShoppingCart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Content(ex.Message, "text/html");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> TryDeleteBasketAsync(string userName)
        {
            try
            {
                await _basketRepository.DeleteBasket(userName);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Content(ex.Message, "text/html");
            }
        }
    }
}