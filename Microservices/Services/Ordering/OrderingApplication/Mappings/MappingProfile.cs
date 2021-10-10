using AutoMapper;
using OrderingDomain.Entities;
using OrderingApplication.Feutures.Orders.Queries.GetOrdersList;
using OrderingApplication.Feutures.Orders.Commands.CheckoutOrder;

namespace OrderingApplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
        }
    }
}
