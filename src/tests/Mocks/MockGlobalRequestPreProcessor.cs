using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockGlobalRequestPreProcessor : IGlobalRequestPreProcessor
    {
        public bool IsGlobalPreProcessorCalled = false;
        public IRequest Request;
        public MockGlobalRequestPreProcessor()
        {
            IsGlobalPreProcessorCalled = false;
        }

        public Task Process(IRequest request)
        {
            Request = request;
            IsGlobalPreProcessorCalled = true;
            return Task.CompletedTask;
        }
    }
}
