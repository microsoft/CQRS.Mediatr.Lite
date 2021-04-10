using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Queries;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Commands;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Controllers
{
    [Route("api/orders")]
    public class OrdersController: ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryService _queryService;

        public OrdersController(ICommandBus commandBus, IQueryService queryService)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            _queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllOrdersQuery query = new();
            IEnumerable<OrderDto> orders = await _queryService.Query(query);
            if (orders == null || !orders.Any())
                return new NotFoundObjectResult("No orders are present in the database");
            return new OkObjectResult(orders);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            GetOrderQuery query = new(id);
            OrderDto order = await _queryService.Query(query);
            if (order == null)
                return new NotFoundObjectResult($"No order with ID {id} are present in the database");
            return new OkObjectResult(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto order)
        {
            CreateOrderCommand command = new(order.Customer, order.Products);
            IdCommandResult result = await _commandBus.Send(command);
            return new CreatedResult($"api/orders/{result.Id}", result.Id);
        }

        [HttpPut]
        [Route("{id}/ship")]
        public async Task<IActionResult> ShipOrder(string id)
        {
            ShipOrderCommand command = new(id);
            await _commandBus.Send(command);
            return new NoContentResult();
        }

        [HttpPut]
        [Route("{id}/deliver")]
        public async Task<IActionResult> DeliverOrder(string id)
        {
            DeliverOrderCommand command = new(id);
            await _commandBus.Send(command);
            return new NoContentResult();
        }
    }
}
