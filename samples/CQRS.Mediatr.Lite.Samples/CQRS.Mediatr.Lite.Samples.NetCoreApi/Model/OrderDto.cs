using System.Collections.Generic;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Model
{
    public class OrderDto
    {
        public string OrderId { get; set; }
        public CustomerDto Customer { get; set; }
        public string Status { get; set; }
        public string CreatedOn { get; set; }
        public string ShippedOn { get; set; }
        public string DeliveredOn { get; set; }
        public string ShippingAddress { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
        public double TotalPrice { get; set; }

    }
}
