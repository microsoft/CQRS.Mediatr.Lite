using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using CQRS.Mediatr.Lite.Exceptions;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class RemoteCommandBusTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task RemoteCommandBus_ShouldExecuteCommand_WhenCommandIsSent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var commandId = Guid.NewGuid().ToString();
            var command = new MockCommand(commandId);
            var commandDictionary = new Dictionary<string, Tuple<Type, Type>>()
            {
                { command.DisplayName, new Tuple<Type, Type>(typeof(MockCommand), typeof(MockCommandResult)) }
            };
            IRemoteCommandBus commandBus = new RemoteCommandBus(handlerResolver, commandDictionary);
            #endregion

            #region Act
            var response = await commandBus.Send(command.DisplayName, JsonConvert.SerializeObject(command));
            MockCommandResult commandResult = response as MockCommandResult;
            #endregion Act

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(commandResult.IsSuccesfull);
            #endregion Assert
        }

        [TestCategory("Integration")]
        [TestMethod]
        [ExpectedException(typeof(HandlerNotFoundException))]
        public async Task RemoteCommandBus_ShouldThrowException_WhenInvalidCommandNameIsSent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var commandId = Guid.NewGuid().ToString();
            var command = new MockCommand(commandId);
            var commandDictionary = new Dictionary<string, Tuple<Type, Type>>()
            {
                { command.DisplayName, new Tuple<Type, Type>(typeof(MockCommand), typeof(MockCommandResult)) }
            };
            IRemoteCommandBus commandBus = new RemoteCommandBus(handlerResolver, commandDictionary);
            #endregion

            #region Act & Assert
            _ = await commandBus.Send(Guid.NewGuid().ToString(), JsonConvert.SerializeObject(command));
            #endregion Act & Assert
        }
    }
}
