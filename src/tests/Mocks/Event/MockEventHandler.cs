using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockEventHandler : EventHandler<MockEvent>
    {
        public bool IsEventHandlerTriggered = false;
        public string EventId;
        protected override Task<VoidResult> ProcessRequest(MockEvent request)
        {
            EventId = request.Id;
            IsEventHandlerTriggered = true;
            return Task.FromResult(new VoidResult());

        }
    }
}
