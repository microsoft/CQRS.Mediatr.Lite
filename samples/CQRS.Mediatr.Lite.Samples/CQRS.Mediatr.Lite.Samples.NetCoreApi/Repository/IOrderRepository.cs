using System;
using System.Collections.Generic;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<OrderDto> GetOrders(Func<OrderDto, bool> predicate);
        void AddOrUpdate(OrderDto order);
    }
}