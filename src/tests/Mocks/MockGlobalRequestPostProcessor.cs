using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockGlobalRequestPostProcessor : IGlobalRequestPostProcessor
    {
        public bool IsGlobalProcessorCalled = false;
        public IRequest Request;
        public object Response;

        public MockGlobalRequestPostProcessor()
        {
            IsGlobalProcessorCalled = false;
        }

        public Task Process(IRequest request, object response)
        {
            IsGlobalProcessorCalled = true;
            Request = request;
            Response = response;
            return Task.CompletedTask;
        }
    }
}
