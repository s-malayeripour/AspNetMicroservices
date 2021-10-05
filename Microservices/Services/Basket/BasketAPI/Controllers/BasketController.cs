using BasketAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using BasketAPI.Repositories.Interfaces;
using BasketAPI.GrpcServices;
using DiscountGRPC.Protos;

namespace BasketAPI.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<BasketController> _logger;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository basketRepository, ILogger<BasketController> logger, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
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
                foreach (ShoppingCartItem shoppingItem in shoppingCart.Items)
                {
                    CouponModel coupon = await _discountGrpcService.GetDiscount(shoppingItem.ProductName);
                    shoppingItem.Price -= coupon.Amount;
                }
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