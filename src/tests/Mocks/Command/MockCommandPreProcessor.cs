using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockCommandPreProcessor : IRequestPreProcessor<MockCommand>
    {
        public bool IsCommandPreProcessorCalled = false;
        public MockCommand Command;

        public Task Process(MockCommand request)
        {
            Command = request;
            IsCommandPreProcessorCalled = true;
            return Task.CompletedTask;
        }
    }
}
