using MediatR;
using AutoMapper;
using OrderingDomain.Entities;
using OrderingApplication.Exceptions;
using OrderingApplication.Contracts.Persistence;

namespace OrderingApplication.Feutures.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order mappedOrder = _mapper.Map<Order>(request);
            IEnumerable<Order> foundOrder = await _orderRepository.GetOrdersByUserName(mappedOrder.Username);
            if (!foundOrder.Any()) throw new NotFoundException(nameof(Order), mappedOrder.Id.ToString());

            await _orderRepository.UpdateAsync(mappedOrder);
            return Unit.Value;
        }
    }
}