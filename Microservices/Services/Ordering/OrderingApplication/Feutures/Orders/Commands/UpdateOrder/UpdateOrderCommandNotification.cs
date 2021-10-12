using MediatR;

namespace OrderingApplication.Feutures.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandNotification : INotification
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
