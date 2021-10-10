using MediatR;
using Microsoft.Extensions.Logging;

namespace OrderingApplication.Feutures.Orders.Commands.CheckoutOrder
{
    internal class OrderCheckedOutLoggerHandler : INotificationHandler<OrderCheckedOutEvent>
    {
        private readonly ILogger _logger;

        public OrderCheckedOutLoggerHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderCheckedOutEvent notification, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, $"Order with ID : {notification.Order.Id} created successfully.");
            return Task.CompletedTask;
        }
    }
}
