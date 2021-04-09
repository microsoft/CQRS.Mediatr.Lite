using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockQueryPostProcessor : IRequestPostProcessor<MockQuery, MockQueryResponse>
    {
        public bool IsQueryPostProcessorCalled = false;
        public MockQuery Query;
        public MockQueryResponse Response;

        public MockQueryPostProcessor()
        {
            IsQueryPostProcessorCalled = false;
        }

        public Task Process(MockQuery request, MockQueryResponse response)
        {
            IsQueryPostProcessorCalled = true;
            Query = request;
            Response = response;
            return Task.CompletedTask;
        }
    }
}
