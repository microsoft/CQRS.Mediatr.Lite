using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Event
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class EventHandlerTests
    {
        [TestMethod]
        public async Task EventHandler_ShouldHandleEvent()
        {
            #region Arrange
            var eventId = Guid.NewGuid().ToString();
            var @event = new MockEvent(eventId);
            var eventHandler = new MockEventHandler();
            #endregion  Arrange

            #region Act
            var voidResult = await eventHandler.Handle(@event);
            #endregion  Act

            #region Assert
            Assert.IsNotNull(voidResult);
            Assert.AreEqual(typeof(VoidResult), voidResult.GetType());
            #endregion Assert
        }
    }
}
