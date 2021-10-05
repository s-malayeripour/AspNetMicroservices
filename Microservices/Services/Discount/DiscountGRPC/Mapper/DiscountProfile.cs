using AutoMapper;
using DiscountGRPC.Entities;
using DiscountGRPC.Protos;

namespace DiscountGRPC.Mapper
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}