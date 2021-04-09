using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockCommandPostProcessor : IRequestPostProcessor<MockCommand, MockCommandResult>
    {
        public bool IsCommandPostProcessorCalled = false;
        public MockCommand Command;
        public MockCommandResult CommandResult;

        public Task Process(MockCommand request, MockCommandResult response)
        {
            IsCommandPostProcessorCalled = true;
            Command = request;
            CommandResult = response;
            return Task.CompletedTask;
        }
    }
}
