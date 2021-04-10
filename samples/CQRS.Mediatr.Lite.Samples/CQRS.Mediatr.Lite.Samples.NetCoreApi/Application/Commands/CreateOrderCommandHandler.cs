using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Repository;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Domain.OrderBoundedContext;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Commands
{
    public class CreateOrderCommandHandler : CommandHandler<CreateOrderCommand, IdCommandResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _eventBus;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IEventBus eventBus)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        protected override async Task<IdCommandResult> ProcessRequest(CreateOrderCommand request)
        {
            OrderAggregateRoot order = new();
            order.CreateOrder(request.Customer, request.Products);
            
            OrderDto orderDto = OrderAssembler.AssembleDto(order);
            _orderRepository.AddOrUpdate(orderDto);
            
            await _eventBus.Send(order.UncommittedEvents);
            return new IdCommandResult(order.Id);
        }
    }
}
