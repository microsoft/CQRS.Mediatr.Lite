using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Internal;
using CQRS.Mediatr.Lite.Exceptions;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using CQRS.Mediatr.Lite.Tests.Common.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Internal
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CommandHandlerWrapperTests
    {
        [TestMethod]
        public async Task CommandHandlerWrapper_ShouldHandleQuery()
        {
            #region Arrange
            var commandId = Guid.NewGuid().ToString();
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var commandHandlerWrapper = new CommandHandlerWrapper<MockCommand, MockCommandResult>();
            var command = new MockCommand(commandId);
            #endregion Arrange

            #region Act
            var response = await commandHandlerWrapper.Handle(command, handlerResolver);
            #endregion Act

            #region Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(commandId, response.CommandId);
            #endregion Assert
        }

        [ExpectedException(typeof(HandlerNotFoundException))]
        [TestMethod]
        public void CommandHandlerWrapper_ShouldThrowHandlerNotFoundException_WhenNoCommandHandlerIsFound()
        {
            #region Arrange
            var commandId = Guid.NewGuid().ToString();
            var mockHandlerGenerator = new MockHandlerGenerator()
            {
                _commandHandler = null
            };
            var handlerResolver = new MockRequestHandlerResolver(mockHandlerGenerator);
            var commandHandlerWrapper = new CommandHandlerWrapper<MockCommand, MockCommandResult>();
            var command = new MockCommand(commandId);
            #endregion Arrange

            #region Act
            AssertExtender.AssertRaisesException<HandlerNotFoundException>(
                () => commandHandlerWrapper.Handle(command, handlerResolver).Wait(),
                expectedMessage: $"No handler of type {typeof(CommandHandler<MockCommand, MockCommandResult>).FullName} was found. Please ensure that the handler has been registered in your dependency resolution.");
            #endregion Act
        }
    }
}
