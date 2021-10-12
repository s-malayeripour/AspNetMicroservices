using MediatR;
using Microsoft.Extensions.Logging;

namespace OrderingApplication.Feutures.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandNotificationHandler : INotificationHandler<UpdateOrderCommandNotification>
    {
        private readonly ILogger _logger;

        public UpdateOrderCommandNotificationHandler(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Handle(UpdateOrderCommandNotification notification, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, $"Order id: {notification.Id} for this user '{notification.Username}' update successfully.");
            return Task.CompletedTask;
        }
    }
}
