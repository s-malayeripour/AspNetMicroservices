using MediatR;

namespace OrderingApplication.Feutures.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteOrderCommand(int id)
        {
            Id = id;
        }
    }
}
