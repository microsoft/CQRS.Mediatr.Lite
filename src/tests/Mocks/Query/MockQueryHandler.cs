using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockQueryHandler : QueryHandler<MockQuery, MockQueryResponse>
    {
        protected override Task<MockQueryResponse> ProcessRequest(MockQuery request)
        {
            return Task.FromResult(new MockQueryResponse() { RequestId = request.Id });
        }
    }
}
