using MediatR;
using AutoMapper;
using OrderingDomain.Entities;
using Microsoft.Extensions.Logging;
using OrderingApplication.Contracts.Persistence;

namespace OrderingApplication.Feutures.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CheckoutOrderCommandHandler(IMapper mapper, ILogger logger, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            Order requestORder = _mapper.Map<Order>(request);
            Order createdOrder = await _orderRepository.AddAsync(requestORder);
            return createdOrder.Id;
        }
    }
}
