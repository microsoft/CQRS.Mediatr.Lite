using System;
using System.Collections.Generic;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Application.Commands
{
    public class CreateOrderCommand : Command<IdCommandResult>
    {
        public override string Id { get; }
        public override string DisplayName => nameof(CreateOrderCommand);

        public CustomerDto Customer { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }

        public CreateOrderCommand(CustomerDto customer, IEnumerable<ProductDto> products)
        {
            Id = Guid.NewGuid().ToString();
            Customer = customer;
            Products = products;
        }

        public override bool Validate(out string ValidationErrorMessage)
        {
            if (Customer == null)
            {   
                ValidationErrorMessage = "Error cannot be placed without a valid customer";
                return false;
            }
            ValidationErrorMessage = null;
            return true;
        }
    }
}
