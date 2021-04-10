using System;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Domain.OrderBoundedContext
{
    public class Customer : ValueObject
    {
        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Id));
                _id = value;
            }
        }


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Name));
                _name = value;
            }
        }
        public string BillingAddress { get; set; }

        public Customer(string customerId, string customerName, string billingAddress) 
        {
            Id = customerId;
            Name = customerName;
            BillingAddress = billingAddress;
        }
    }
}
