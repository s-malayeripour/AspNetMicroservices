using DiscountGRPC.Protos;

namespace BasketAPI.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            return await _discountProtoService.GetDiscountAsync(new GetDiscountRequest()
            {
                ProductName = productName
            });
        }
    }
}
