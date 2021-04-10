using System;

namespace CQRS.Mediatr.Lite.Samples.NetCoreApi.Domain.OrderBoundedContext
{
    public class Product: ValueObject
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

        public string Description { get; private set; }

        private double _price;
        public double Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Price cannot be negative", nameof(Price));
                _price = value;
            }
        }

        public Product(string productId, string productName, string productDescription)
        {
            Id = productId;
            Name = productName;
            Description = productDescription;
        }
    }
}
