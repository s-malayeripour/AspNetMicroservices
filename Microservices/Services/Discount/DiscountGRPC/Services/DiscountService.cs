using AutoMapper;
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
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            return base.GetDiscount(request, context);

        }

        public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            try
            {
                Coupon requestCoupon = _mapper.Map<Coupon>(request.Coupon);
                await _discountRepository.CreateDiscount(requestCoupon);
                return _mapper.Map<CouponModel>(requestCoupon);
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }

        public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            try
            {
                Coupon requestCoupon = _mapper.Map<Coupon>(request.Coupon);
                await _discountRepository.UpdateDiscount(requestCoupon);
                return _mapper.Map<CouponModel>(requestCoupon);
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }

        public async override Task<DeleteDiscountResult> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            try
            {
                bool isDelted = await _discountRepository.DeleteDiscount(request.ProductName);
                return new DeleteDiscountResult()
                {
                    IsDeleted = isDelted
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }
    }
}
