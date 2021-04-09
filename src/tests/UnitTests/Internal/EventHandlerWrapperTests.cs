using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Internal;
using CQRS.Mediatr.Lite.Exceptions;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using CQRS.Mediatr.Lite.Tests.Common.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Internal
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class EventHandlerWrapperTests
    {
        [TestMethod]
        public async Task EventHandlerWrapper_ShouldHandleEvent()
        {
            #region Arrange
            var eventId = Guid.NewGuid().ToString();
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var eventHandlerWrapper = new EventHandlerWrapper<MockEvent>();
            var @event = new MockEvent(eventId);
            #endregion Arrange

            #region Act
            await eventHandlerWrapper.Handle(@event, handlerResolver);
            #endregion Act

            #region Assert
            mockHandlerGenerator.VerifyEventHandlers(eventId);
            #endregion Assert
        }

        [TestMethod]
        public async Task EventHandlerWrapper_ShouldNotHandleEvent_WhenNoEventHanderIsPresent()
        {
            #region Arrange
            var eventId = Guid.NewGuid().ToString();
            var mockHandlerGenerator = new MockHandlerGenerator()
            {
                _eventHandlers = null
            };
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var eventHandlerWrapper = new EventHandlerWrapper<MockEvent>();
            var @event = new MockEvent(eventId);
            #endregion Arrange

            #region Act
            await eventHandlerWrapper.Handle(@event, handlerResolver);
            #endregion Act
        }

        [TestMethod]
        public async Task EventHandlerWrapper_ShouldNotHandleEvent_WhenNullEventHandersAreReturned()
        {
            #region Arrange
            var eventId = Guid.NewGuid().ToString();
            var mockHandlerGenerator = new MockHandlerGenerator()
            {
                _eventHandlers = null
            };
            var handlerResolver = new MockRequestHandlerResolver(mockHandlerGenerator);
            var eventHandlerWrapper = new EventHandlerWrapper<MockEvent>();
            var @event = new MockEvent(eventId);
            #endregion Arrange

            #region Act
            await eventHandlerWrapper.Handle(@event, handlerResolver);
            #endregion Act
        }

        [TestMethod]
        public async Task EventHandlerWrapper_ShouldNotHandleEvent_WhenEmptyEventHandersAreReturned()
        {
            #region Arrange
            var eventId = Guid.NewGuid().ToString();
            var mockHandlerGenerator = new MockHandlerGenerator()
            {
                _eventHandlers = new List<EventHandler<MockEvent>>()
            };
            var handlerResolver = new MockRequestHandlerResolver(mockHandlerGenerator);
            var eventHandlerWrapper = new EventHandlerWrapper<MockEvent>();
            var @event = new MockEvent(eventId);
            #endregion Arrange

            #region Act
            await eventHandlerWrapper.Handle(@event, handlerResolver);
            #endregion Act
        }
    }
}
