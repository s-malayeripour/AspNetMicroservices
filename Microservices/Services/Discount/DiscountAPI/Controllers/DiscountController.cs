using DiscountAPI.Entities;
using DiscountAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiscountAPI.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }

        [HttpGet]
        public async Task<ActionResult<Coupon>> TryGetDiscountAsync(string productName)
        {
            try
            {
                Coupon resultCoupon = await _discountRepository.GetDiscount(productName);
                return Ok(resultCoupon);
            }
            catch (Exception ex)
            {
                return Content(ex.Message, "text/html");
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> TryCreateDiscountAsync([FromBody] Coupon coupon)
        {
            try
            {
                bool isCreated = await _discountRepository.CreateDiscount(coupon);
                return Ok(isCreated);
            }
            catch (Exception ex)
            {
                return Content(ex.Message, "text/html");
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> TryUpdateDiscountAsync([FromBody] Coupon coupon)
        {
            try
            {
                bool isUpdated = await _discountRepository.UpdateDiscount(coupon);
                return Ok(isUpdated);
            }
            catch (Exception ex)
            {
                return Content(ex.Message, "text/html");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> TryDeleteDiscountAsync(string productName)
        {
            try
            {
                bool isDeleted = await _discountRepository.DeleteDiscount(productName);
                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                return Content(ex.Message, "text/html");
            }
        }
    }
}
