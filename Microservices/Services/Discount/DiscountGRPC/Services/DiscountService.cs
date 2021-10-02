using DiscountGRPC.Entities;
using DiscountGRPC.Protos;
using DiscountGRPC.Repositories.Interfaces;
using Grpc.Core;

namespace DiscountGRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger)
        {
            _discountRepository = discountRepository;
            _logger = logger;
        }

        public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            try
            {
                Coupon resultCoupon = await _discountRepository.GetDiscount(request.ProductName);
                if (resultCoupon is null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, $"Discount for product name : {request.ProductName} not found."));
                }
                return new CouponModel
                {
                    Id = resultCoupon.Id,
                    ProductName = resultCoupon.ProductName,
                    Description = resultCoupon.Description,
                    Amount = resultCoupon.Amount
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }
    }
}
