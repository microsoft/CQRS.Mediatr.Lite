using System;
using CQRS.Mediatr.Lite.Exceptions;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CQRS.Mediatr.Lite.Tests.Common.TestHelper;
using System.Collections.Generic;
using System.Linq;

namespace CQRS.Mediatr.Lite.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class RequestHandlerResolverTests
    {
        #region Positive Scenarios
        [TestMethod]
        public void RequestHandler_ShouldResolveCommandHandler_WhenCommandHandlerIsAvailable()
        {
            #region Arrange
            var mockHandler = new MockCommandHandler();
            object resolver(Type type) => mockHandler;
            var requestHandlerResolver = new RequestHandlerResolver(resolver);
            #endregion Arrange

            #region Act
            var resolvedHandler = requestHandlerResolver.Resolve<MockCommandHandler>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(resolvedHandler);
            Assert.AreSame(mockHandler, resolvedHandler);
            #endregion Assert
        }

        [TestMethod]
        public void RequestHandler_ShouldResolveQueryHandler_WhenQueryHandlerIsAvailable()
        {
            #region Arrange
            var mockHandler = new MockQueryHandler();
            object resolver(Type type) => mockHandler;
            var requestHandlerResolver = new RequestHandlerResolver(resolver);
            #endregion Arrange

            #region Act
            var resolvedHandler = requestHandlerResolver.Resolve<MockQueryHandler>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(resolvedHandler);
            Assert.AreSame(mockHandler, resolvedHandler);
            #endregion Assert
        }

        [TestMethod]
        public void RequestHandler_ShouldResolveEventHandler_WhenEvntHandlerIsAvailable()
        {
            #region Arrange
            var mockHandler = new MockEventHandler();
            object resolver(Type type) => mockHandler;
            var requestHandlerResolver = new RequestHandlerResolver(resolver);
            #endregion Arrange

            #region Act
            var resolvedHandler = requestHandlerResolver.Resolve<MockEventHandler>();
            #endregion Act

            #region Assert
            Assert.IsNotNull(resolvedHandler);
            Assert.AreSame(mockHandler, resolvedHandler);
            #endregion Assert
        }

        [TestMethod]
        public void RequestHandler_ShouldResolveEventHandlers_WhenEventHandlersAreAvailable()
        {
            #region Arrange
            var mockHandler1 = new MockEventHandler();
            var mockHandler2 = new MockEventHandler2();
            var mockHandlers = new List<EventHandler<MockEvent>>() { mockHandler1, mockHandler2 };
            object resolver(Type type) => mockHandlers;
            var requestHandlerResolver = new RequestHandlerResolver(resolver);
            #endregion Arrange

            #region Act
            var resolvedHandlers = requestHandlerResolver.ResolveAll<EventHandler<MockEvent>>()
                .ToList();
            #endregion Act

            #region Assert
            Assert.IsNotNull(resolvedHandlers);
            Assert.AreEqual(mockHandlers.Count, resolvedHandlers.Count);
            for (var handlerIterator = 0; handlerIterator < mockHandlers.Count; handlerIterator++)
            {
                Assert.AreSame(mockHandlers[handlerIterator], resolvedHandlers[handlerIterator]);
            }
            #endregion Assert
        }
        #endregion  Positive Scenarios

        [ExpectedException(typeof(HandlerNotFoundException))]
        [TestMethod]
        public void RequestHandler_ShouldThrowHandlerNotFoundException_WhenHandlerIsNotAvailable()
        {
            #region Arrange
            object resolver(Type type) => null;
            var requestHandlerResolver = new RequestHandlerResolver(resolver);
            #endregion Arrange

            #region Act
            var resolvedHandler = requestHandlerResolver.Resolve<MockCommandHandler>();
            #endregion Act
        }

        [ExpectedException(typeof(HandlerResolutionException))]
        [TestMethod]
        public void RequestHandler_ShouldThrowHandlerResolutionException_WhenHandlerResolutionBreaks()
        {
            #region Arrange
            var dummyException = new Exception("Dummy Exception");
            object resolver(Type type) => throw dummyException;
            var expectedMessage = $"There was an error in creating the handler of type {typeof(MockCommandHandler).FullName}. Please check the dependency resolution module.";
            var requestHandlerResolver = new RequestHandlerResolver(resolver);
            #endregion Arrange

            #region Act
            AssertExtender.AssertRaisesException<HandlerResolutionException>(
                () => requestHandlerResolver.Resolve<MockCommandHandler>(),
                expectedMessage: expectedMessage,
                expectedInnerException: dummyException);
            #endregion Act
        }

        [ExpectedException(typeof(HandlerNotFoundException))]
        [TestMethod]
        public void RequestHandler_ShouldThrowHandlerNotFoundException_OnResolveAll_WhenNoHandlersAreFound()
        {
            #region Arrange
            object resolver(Type type) => null;
            var requestHandlerResolver = new RequestHandlerResolver(resolver);
            #endregion Arrange

            #region Act
            var resolvedHandler = requestHandlerResolver.ResolveAll<MockCommandHandler>();
            #endregion Act
        }

        [ExpectedException(typeof(HandlerNotFoundException))]
        [TestMethod]
        public void RequestHandler_ShouldThrowHandlerNotFoundException_OnResolveAll_WhenEmptyHandlersAreResolver()
        {
            #region Arrange
            object resolver(Type type) => new List<MockCommandHandler>();
            var requestHandlerResolver = new RequestHandlerResolver(resolver);
            #endregion Arrange

            #region Act
            var resolvedHandler = requestHandlerResolver.ResolveAll<MockCommandHandler>();
            #endregion Act
        }

        [ExpectedException(typeof(HandlerResolutionException))]
        [TestMethod]
        public void RequestHandler_ShouldThrowHandlerResolutionException__OnResolveAll_WhenHandlerResolutionBreaks()
        {
            #region Arrange
            var dummyException = new Exception("Dummy Exception");
            object resolver(Type type) => throw dummyException;
            var expectedMessage = $"There was an error in creating the handler of type {typeof(IEnumerable<MockCommandHandler>).FullName}. Please check the dependency resolution module.";
            var requestHandlerResolver = new RequestHandlerResolver(resolver);
            #endregion Arrange

            #region Act
            AssertExtender.AssertRaisesException<HandlerResolutionException>(
                () => requestHandlerResolver.ResolveAll<MockCommandHandler>(),
                expectedMessage: expectedMessage,
                expectedInnerException: dummyException);
            #endregion Act
        }
    }
}
