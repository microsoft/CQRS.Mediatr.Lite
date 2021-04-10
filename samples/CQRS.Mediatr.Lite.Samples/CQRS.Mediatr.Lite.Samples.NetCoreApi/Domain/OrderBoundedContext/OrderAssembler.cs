using System.Linq;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Domain.OrderBoundedContext
{
    public class OrderAssembler
    {
        public static OrderAggregateRoot AssembleAggregateRoot(OrderDto orderDto)
        {
            var orderAggregateRoot = new OrderAggregateRoot(
                orderId: orderDto.OrderId,
                orderStatus: orderDto.Status,
                customerId: orderDto.Customer?.CustomerId,
                customerName: orderDto.Customer?.CustomerName,
                billingAddress: orderDto.Customer?.CustomerBillingAddress,
                createdOn: orderDto.CreatedOn,
                shippedOn: orderDto.ShippedOn,
                deliveredOn: orderDto.DeliveredOn,
                shippingAddress: orderDto.ShippingAddress);

            if (orderDto.Products != null && orderDto.Products.Any())
            {
                foreach (ProductDto product in orderDto.Products)
                {
                    orderAggregateRoot.Products.Add(new Product(product.ProductId, product.ProductName, product.ProductDescription));
                }
            }

            return orderAggregateRoot;
        }

        public static OrderDto AssembleDto(OrderAggregateRoot order)
        {
            var orderDto = new OrderDto
            {
                OrderId = order.Id,
                Status = order.Status.Status,
                Customer = new CustomerDto
                {
                    CustomerId = order.Customer?.Id,
                    CustomerName = order.Customer?.Name,
                    CustomerBillingAddress = order.Customer?.BillingAddress
                },
                CreatedOn = order.CreationDate?.ToString(),
                ShippedOn = order.ShippedDate?.ToString(),
                DeliveredOn = order.DeliveryDate?.ToString(),
                ShippingAddress = order.ShippingAddress,
                TotalPrice = order.Cost
            };

            if (order.Products != null && order.Products.Any())
            {
                orderDto.Products = order.Products.Select(product => new ProductDto
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    Price = product.Price
                });
            }

            return orderDto;
        }
    }
}
