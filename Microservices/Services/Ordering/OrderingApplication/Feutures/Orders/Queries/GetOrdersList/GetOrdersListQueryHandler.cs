using MediatR;
using AutoMapper;
using OrderingDomain.Entities;
using OrderingApplication.Contracts.Persistence;

namespace OrderingApplication.Feutures.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderDTO>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            List<Order> orders = (await _orderRepository.GetOrdersByUserName(request.UserName)).ToList();
            List<OrderDTO> ordersConvertedList = _mapper.Map<List<OrderDTO>>(orders);
            return ordersConvertedList;
        }
    }
}
