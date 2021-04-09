using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockEvent : Event
    {
        public override string DisplayName => "Mock Event";
        
        public override string Id { get; set; }

        public MockEvent(string eventId)
        {
            Id = eventId;
        }
    }
}
