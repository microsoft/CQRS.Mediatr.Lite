using Autofac;
using System.Collections.Generic;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Repository;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Queries;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Commands;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Domain.OrderBoundedContext;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Events.EventStoreHandlers;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi
{
    public class CqrsSampleModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Infrastructure
            builder.RegisterType<InMemOrderRepository>()
                .As<IOrderRepository>()
                .SingleInstance();
            builder.RegisterType<InMemEventStore>()
                .As<IEventStore>()
                .SingleInstance();

            // CQRS Resolver
            builder.Register(ctx =>
            {
                var componentContext = ctx.Resolve<IComponentContext>();
                return new RequestHandlerResolver(requiredType => componentContext.Resolve(requiredType));
            })
          .As<IRequestHandlerResolver>();

            // Queries
            builder.RegisterType<GetAllOrderQueryHandler>()
                .As<QueryHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>>()
                .SingleInstance();
            builder.RegisterType<GetOrderQueryHandler>()
                .As<QueryHandler<GetOrderQuery, OrderDto>>()
                .SingleInstance();
            builder.RegisterType<QueryService>()
                .As<IQueryService>();
                

            // Events
            builder.RegisterType<OrderCreatedEventHandler>()
                .As<EventHandler<OrderCreated>>()
                .SingleInstance();
            builder.RegisterType<OrderShippedEventHandler>()
                .As<EventHandler<OrderShipped>>()
                .SingleInstance();
            builder.RegisterType<OrderDeliveredEventHandler>()
                .As<EventHandler<OrderDelivered>>()
                .SingleInstance();
            builder.RegisterType<EventBus>()
                .As<IEventBus>();

            // Commands
            builder.RegisterType<CreateOrderCommandHandler>()
                .As<CommandHandler<CreateOrderCommand, IdCommandResult>>()
                .SingleInstance();
            builder.RegisterType<ShipOrderCommandHandler>()
                .As<CommandHandler<ShipOrderCommand, IdCommandResult>>()
                .SingleInstance();
            builder.RegisterType<DeliverOrderCommandHandler>()
                .As<CommandHandler<DeliverOrderCommand, IdCommandResult>>()
                .SingleInstance();
            builder.RegisterType<CommandBus>()
                .As<ICommandBus>();
        }
    }
}
