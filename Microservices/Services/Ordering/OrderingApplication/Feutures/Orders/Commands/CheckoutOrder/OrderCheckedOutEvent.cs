using MediatR;
using OrderingApplication.Feutures.Orders.Queries.GetOrdersList;

namespace OrderingApplication.Feutures.Orders.Commands.CheckoutOrder
{
    public class OrderCheckedOutEvent : INotification
    {
        public OrderDTO Order { get; }

        public OrderCheckedOutEvent(OrderDTO order)
        {
            Order = order;
        }
    }
}
