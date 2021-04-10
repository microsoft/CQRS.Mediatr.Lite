using System;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Repository;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Domain.OrderBoundedContext;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Commands
{
    public class ShipOrderCommandHandler : CommandHandler<ShipOrderCommand, IdCommandResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _eventBus;

        public ShipOrderCommandHandler(IOrderRepository orderRepository, IEventBus eventBus)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        protected override async Task<IdCommandResult> ProcessRequest(ShipOrderCommand request)
        {
            OrderDto orderDto = _orderRepository.GetOrders(order => order.OrderId == request.OrderId).FirstOrDefault();
            if (orderDto == null)
                throw new Exception("Invalid Order ID");

            OrderAggregateRoot order = OrderAssembler.AssembleAggregateRoot(orderDto);
            order.ShipOrder();
            orderDto = OrderAssembler.AssembleDto(order);
            _orderRepository.AddOrUpdate(orderDto);
            await _eventBus.Send(order.UncommittedEvents);
            return new IdCommandResult(order.Id);
        }
    }
}
