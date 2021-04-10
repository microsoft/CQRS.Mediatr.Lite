using System;
using System.Collections.Generic;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Queries
{
    public class GetAllOrdersQuery : Query<IEnumerable<OrderDto>>
    {
        public override string DisplayName => nameof(GetAllOrdersQuery);

        public override string Id { get; }

        public GetAllOrdersQuery()
        {
            Id = Guid.NewGuid().ToString();
        }

        public override bool Validate(out string ValidationErrorMessage)
        {
            ValidationErrorMessage = null;
            return true;
        }
    }
}
