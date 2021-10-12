using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingApplication.Feutures.Orders.Queries.GetOrdersList;
using OrderingApplication.Feutures.Orders.Commands.CheckoutOrder;
using OrderingApplication.Feutures.Orders.Commands.DeleteOrder;
using OrderingApplication.Feutures.Orders.Commands.UpdateOrder;

namespace OrderingAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByUsername(string userName)
        {
            GetOrdersListQuery query = new GetOrdersListQuery(userName);
            var result = await _mediator.Send(userName);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand orderCommand)
        {
            var result = await _mediator.Send(orderCommand);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            DeleteOrderCommand command = new DeleteOrderCommand(orderId);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand orderCommand)
        {
            await _mediator.Send(orderCommand);
            return NoContent();
        }
    }
}
