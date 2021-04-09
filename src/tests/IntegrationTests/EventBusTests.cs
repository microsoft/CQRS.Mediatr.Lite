using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class EventBusTests
    {   
        [TestMethod]
        public async Task EventBus_ShouldSendEvent_WhenEventIsSent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var eventBus = new EventBus(handlerResolver);
            var eventId = Guid.NewGuid().ToString();
            var @event = new MockEvent(eventId);
            #endregion Arrange

            #region Act
            await eventBus.Send(@event);
            #endregion Act

            #region Assert
            mockHandlerGenerator.VerifyEventHandlers(eventId);
            #endregion Assert
        }

        [TestMethod]
        public async Task EventBus_ShouldSendEvent_WhenEventStreamIsSent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var eventBus = new EventBus(handlerResolver);
            var eventId = Guid.NewGuid().ToString();
            var @event = new MockEvent(eventId);
            var @event_2 = new MockEvent(eventId);
            var eventStream = new List<MockEvent>() { @event, @event_2 };
            #endregion Arrange

            #region Act
            await eventBus.Send(eventStream);
            #endregion Act

            #region Assert
            mockHandlerGenerator.VerifyEventHandlers(eventId);
            #endregion Assert
        }
    }


}
