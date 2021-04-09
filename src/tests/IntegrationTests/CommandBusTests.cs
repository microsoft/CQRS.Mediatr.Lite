using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CommandBusTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task CommandBus_ShouldExecuteCommand_WhenCommandIsSent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var commandBus = new CommandBus(handlerResolver);
            var commandId = Guid.NewGuid().ToString();
            var command = new MockCommand(commandId);
            #endregion

            #region Act
            var response = await commandBus.Send(command);
            #endregion Act

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccesfull);
            Assert.AreEqual(commandId, response.CommandId, message: "Response ID and Command ID doesn't match");
            mockHandlerGenerator.VerifyGlobalProcessors(commandId);
            mockHandlerGenerator.VerifyCommandProcessors(commandId);
            #endregion Assert
        }
    }
}
