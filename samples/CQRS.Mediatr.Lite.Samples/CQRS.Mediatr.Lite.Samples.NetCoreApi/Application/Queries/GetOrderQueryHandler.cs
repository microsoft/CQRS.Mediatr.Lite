using System;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Repository;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Queries
{
    public class GetOrderQueryHandler: QueryHandler<GetOrderQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        protected override Task<OrderDto> ProcessRequest(GetOrderQuery request)
        {
            return Task.FromResult(_orderRepository.GetOrders(order => order.OrderId == request.OrderId).FirstOrDefault());
        }
    }
}
