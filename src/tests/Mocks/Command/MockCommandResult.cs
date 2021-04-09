using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockCommandResult : CommandResult
    {
        public string CommandId { get; set; }
        public MockCommandResult(bool isSuccesfull, string commandId) : base(isSuccesfull)
        {
            CommandId = commandId;
        }
    }
}
