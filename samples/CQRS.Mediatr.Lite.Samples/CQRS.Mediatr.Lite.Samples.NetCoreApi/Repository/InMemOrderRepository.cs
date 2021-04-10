using System;
using System.Linq;
using System.Collections.Generic;
using CQRS.Mediatr.Lite.Samples.NetCoreApi.Model;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Repository
{
    public class InMemOrderRepository : IOrderRepository
    {
        private static readonly IList<OrderDto> _orders = new List<OrderDto>();
        public InMemOrderRepository()
        {
            Seed();
        }

        public IEnumerable<OrderDto> GetOrders(Func<OrderDto, bool> predicate)
        {
            return _orders.Where(predicate);
        }

        public void AddOrUpdate(OrderDto newOrder)
        {
            OrderDto existingOrder = _orders.FirstOrDefault(order => order.OrderId == newOrder.OrderId);
            if (existingOrder != null)
            {
                existingOrder = newOrder;
            }
            else
            {
                _orders.Add(newOrder);
            }
        }

        private void Seed()
        {
            _orders.Add(new OrderDto
            {
                OrderId = Guid.NewGuid().ToString(),
                Status = "Delivered",
                CreatedOn = DateTime.UtcNow.AddDays(-15).ToString(),
                ShippedOn = DateTime.UtcNow.AddDays(-14).ToString(),
                DeliveredOn = DateTime.UtcNow.AddDays(-5).ToString(),
                Customer = new CustomerDto
                {
                    CustomerId = Guid.NewGuid().ToString(),
                    CustomerName = "Reema Khanna",
                    CustomerBillingAddress = "661/B Girish Palace Navi Mumbai"
                },
                Products = new List<ProductDto>
                {
                    new ProductDto
                    {
                        ProductId = Guid.NewGuid().ToString(),
                        ProductName = "Book: Advanced Statistics in Machine Learning",
                        ProductDescription = "Book on statistics",
                        Price = 599.00
                    }
                },
                ShippingAddress = "661/B Girish Palace Navi Mumbai",
                TotalPrice = 599.00
            });

            _orders.Add(new OrderDto
            {
                OrderId = Guid.NewGuid().ToString(),
                Status = "Placed",
                CreatedOn = DateTime.UtcNow.AddDays(-1).ToString(),
                ShippedOn = null,
                DeliveredOn = null,
                Customer = new CustomerDto
                {
                    CustomerId = Guid.NewGuid().ToString(),
                    CustomerName = "Pratik Bhattacharya",
                    CustomerBillingAddress = "99/A Chowk Place Road Kolkata"
                },
                Products = new List<ProductDto>
                {
                    new ProductDto
                    {
                        ProductId = Guid.NewGuid().ToString(),
                        ProductName = "Dell All-in-One",
                        ProductDescription = "Computers",
                        Price = 165000.00
                    }
                },
                ShippingAddress = "Flat 501 Nizam Palace Hyderabad",
                TotalPrice = 165000.00
            });

            _orders.Add(new OrderDto
            {
                OrderId = Guid.NewGuid().ToString(),
                Status = "Shipped",
                CreatedOn = DateTime.UtcNow.AddDays(-3).ToString(),
                ShippedOn = DateTime.UtcNow.AddDays(-1).ToString(),
                DeliveredOn = null,
                Customer = new CustomerDto
                {
                    CustomerId = Guid.NewGuid().ToString(),
                    CustomerName = "Sneha Gupta",
                    CustomerBillingAddress = "199 6/B Kuloor Market"
                },
                Products = new List<ProductDto>
                {
                    new ProductDto
                    {
                        ProductId = Guid.NewGuid().ToString(),
                        ProductName = "Dell All-in-One",
                        ProductDescription = "Computers",
                        Price = 165000.00
                    },
                    new ProductDto
                    {
                        ProductId = Guid.NewGuid().ToString(),
                        ProductName = "Mouse Pad",
                        ProductDescription = "Computers Accessories",
                        Price = 250.00
                    }
                },
                ShippingAddress = "199 6/B Kuloor Market",
                TotalPrice = 165250.00
            });
        }
    }
}
