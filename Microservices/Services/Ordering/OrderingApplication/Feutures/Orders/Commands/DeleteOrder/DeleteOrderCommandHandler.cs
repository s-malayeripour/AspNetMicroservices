using MediatR;
using OrderingApplication.Contracts.Persistence;

namespace OrderingApplication.Feutures.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            bool isDeleted = await _orderRepository.DeleteOrderByIdAsync(request.Id);
            return isDeleted;
        }
    }
}
