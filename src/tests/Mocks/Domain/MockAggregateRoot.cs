using CQRS.Mediatr.Lite.SDK.Domain;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks.Domain
{
    [ExcludeFromCodeCoverage]
    internal class MockAggregateRoot: AggregateRoot
    {
        public MockAggregateRoot(string id): base(id) {  }
    }
}
