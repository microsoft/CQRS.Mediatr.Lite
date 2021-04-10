using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Repository;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Queries
{
    public class GetAllOrderQueryHandler : QueryHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        protected override Task<IEnumerable<OrderDto>> ProcessRequest(GetAllOrdersQuery request)
        {
            return Task.FromResult(_orderRepository.GetOrders(order => true));
        }
    }
}
