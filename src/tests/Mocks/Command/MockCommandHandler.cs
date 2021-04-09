using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockCommandHandler : CommandHandler<MockCommand, MockCommandResult>
    {
        protected override Task<MockCommandResult> ProcessRequest(MockCommand request)
        {
            return Task.FromResult(new MockCommandResult(true, request.Id));
        }
    }
}
