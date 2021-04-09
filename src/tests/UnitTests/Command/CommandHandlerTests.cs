using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Exceptions;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using CQRS.Mediatr.Lite.Tests.Common.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Command
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CommandHandlerTests
    {
        [TestMethod]
        public async Task CommandHandler_ShouldHandleCommand()
        {
            #region Arrange
            var commandId = Guid.NewGuid().ToString();
            var command = new MockCommand(commandId);
            var commandHandler = new MockCommandHandler();
            #endregion  Arrange

            #region Act
            var mockResponse = await commandHandler.Handle(command);
            #endregion  Act

            #region Assert
            Assert.IsNotNull(mockResponse);
            Assert.AreEqual(commandId, mockResponse.CommandId);
            #endregion Assert
        }

        [ExpectedException(typeof(RequestValidationException))]
        [TestMethod]
        public void CommandHandler_ShouldThrowRequestValidationException_WhenCommandValidationFails()
        {
            #region Arrange
            var commandId = Guid.NewGuid().ToString();
            var mockValidationMessage = "Moc validation failed";
            var command = new MockCommand(commandId, mockValidationMessage);
            var commandHandler = new MockCommandHandler();
            #endregion  Arrange

            #region Act
            AssertExtender.AssertRaisesException<RequestValidationException>(
                () => commandHandler.Handle(command).Wait(),
                expectedMessage: $"Request Validataion failed for request {command.DisplayName} with message: {mockValidationMessage}");
            #endregion Act
        }
    }
}
