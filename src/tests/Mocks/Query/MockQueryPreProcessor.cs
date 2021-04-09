using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockQueryPreProcessor : IRequestPreProcessor<MockQuery>
    {
        public bool IsQueryPreProcessorCalled = false;
        public MockQuery Query;

        public MockQueryPreProcessor()
        {
            IsQueryPreProcessorCalled = false;
        }

        public Task Process(MockQuery request)
        {
            IsQueryPreProcessorCalled = true;
            Query = request;
            return Task.CompletedTask;
        }
    }
}
