using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Internal;
using CQRS.Mediatr.Lite.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Internal
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class RequestProcessingManagerTests
    {
        #region Query Processing
        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPreprocessing_ForQueryHandlers()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPreProcessing<MockQuery, MockQueryResponse>(query);
            #endregion Act

            #region Assert
            mockHandlerGenerator.VerifyGlobalPreProcessors(queryId);
            mockHandlerGenerator.VerifyQueryPreProcessors(queryId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPostprocessing_ForQueryHandlers()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            var queryResponse = new MockQueryResponse()
            {
                RequestId = queryId
            };
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPostProcessing<MockQuery, MockQueryResponse>(query, queryResponse);
            #endregion Act

            #region Assert
            mockHandlerGenerator.VerifyGlobalPostProcessors(queryId);
            mockHandlerGenerator.VerifyQueryPostProcessors(queryId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPreprocessing_ForQueryHandlers_WhenHandlerNotFoundExceptionIsThrown()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _globalPreprocessors = null,
                _globalPostProcessors = null

            };
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPreProcessing<MockQuery, MockQueryResponse>(query);
            #endregion Act

            #region Assert            
            mockHandlerGenerator.VerifyQueryPreProcessors(queryId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPreprocessing_ForQueryHandlers_WhenNoGlobalProcessorsArePresent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _globalPreprocessors = null,
                _globalPostProcessors = null

            };
            var handlerResolver = new MockRequestHandlerResolver(mockHandlerGenerator);
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPreProcessing<MockQuery, MockQueryResponse>(query);
            #endregion Act

            #region Assert            
            mockHandlerGenerator.VerifyQueryPreProcessors(queryId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPreprocessing_ForQueryHandlers_WhenZeroGlobalProcessorsArePresent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _globalPreprocessors = new List<IGlobalRequestPreProcessor>(),
                _globalPostProcessors = new List<IGlobalRequestPostProcessor>()

            };
            var handlerResolver = new MockRequestHandlerResolver(mockHandlerGenerator);
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPreProcessing<MockQuery, MockQueryResponse>(query);
            #endregion Act

            #region Assert            
            mockHandlerGenerator.VerifyQueryPreProcessors(queryId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPreprocessing_ForQueryHandlers_WhenNoQueryProcessorsArePresent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _queryPreProcessors = null,
                _queryPostProcessors = null,

            };
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPreProcessing<MockQuery, MockQueryResponse>(query);
            #endregion Act

            #region Assert            
            mockHandlerGenerator.VerifyGlobalPreProcessors(queryId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPostprocessing_ForQueryHandlers_WhenHandlerNotFoundExceptionIsThrown()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _globalPreprocessors = null,
                _globalPostProcessors = null

            };
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            var queryResponse = new MockQueryResponse()
            {
                RequestId = queryId
            };
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPostProcessing<MockQuery, MockQueryResponse>(query, queryResponse);
            #endregion Act

            #region Assert
            mockHandlerGenerator.VerifyQueryPostProcessors(queryId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPostprocessing_ForQueryHandlers_WhenNoGlobalProcessorsArePresent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _globalPreprocessors = null,
                _globalPostProcessors = null

            };
            var handlerResolver = new MockRequestHandlerResolver(mockHandlerGenerator);
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            var queryResponse = new MockQueryResponse()
            {
                RequestId = queryId
            };
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPostProcessing<MockQuery, MockQueryResponse>(query, queryResponse);
            #endregion Act

            #region Assert
            mockHandlerGenerator.VerifyQueryPostProcessors(queryId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPostprocessing_ForQueryHandlers_WhenZeroGlobalProcessorsArePresent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _globalPreprocessors = new List<IGlobalRequestPreProcessor>(),
                _globalPostProcessors = new List<IGlobalRequestPostProcessor>()

            };
            var handlerResolver = new MockRequestHandlerResolver(mockHandlerGenerator);
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            var queryResponse = new MockQueryResponse()
            {
                RequestId = queryId
            };
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPostProcessing<MockQuery, MockQueryResponse>(query, queryResponse);
            #endregion Act

            #region Assert
            mockHandlerGenerator.VerifyQueryPostProcessors(queryId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPostprocessing_ForQueryHandlers_WhenNoQueryProcessorsArePresent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _queryPreProcessors = null,
                _queryPostProcessors = null,

            };
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var queryId = Guid.NewGuid().ToString();
            var query = new MockQuery(queryId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPreProcessing<MockQuery, MockQueryResponse>(query);
            #endregion Act

            #region Assert            
            mockHandlerGenerator.VerifyGlobalPreProcessors(queryId);
            #endregion Assert
        }
        #endregion Query Processing

        #region Command Processing
        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPreprocessing_ForCommandHandlers()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var commandId = Guid.NewGuid().ToString();
            var command = new MockCommand(commandId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPreProcessing<MockCommand, MockCommandResult>(command);
            #endregion Act

            #region Assert
            mockHandlerGenerator.VerifyGlobalPreProcessors(commandId);
            mockHandlerGenerator.VerifyCommandPreProcessors(commandId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPostprocessing_ForCommandHandlers()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator();
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var commandId = Guid.NewGuid().ToString();
            var command = new MockCommand(commandId);
            var commandResponse = new MockCommandResult(true, commandId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPostProcessing<MockCommand, MockCommandResult>(command, commandResponse);
            #endregion Act

            #region Assert
            mockHandlerGenerator.VerifyGlobalPostProcessors(commandId);
            mockHandlerGenerator.VerifyCommandPostProcessors(commandId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPreprocessing_ForCommandHandlers_WhenNoGlobalProcessorsArePresent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _globalPreprocessors = null,
                _globalPostProcessors = null

            };
            var handlerResolver = new MockRequestHandlerResolver(mockHandlerGenerator);
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var commandId = Guid.NewGuid().ToString();
            var command = new MockCommand(commandId);
            var commandResponse = new MockCommandResult(true, commandId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPreProcessing<MockCommand, MockCommandResult>(command);
            #endregion Act

            #region Assert            
            mockHandlerGenerator.VerifyCommandPreProcessors(commandId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPreprocessing_ForCommandHandlers_WhenZeroGlobalProcessorsArePresent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _globalPreprocessors = new List<IGlobalRequestPreProcessor>(),
                _globalPostProcessors = new List<IGlobalRequestPostProcessor>()

            };
            var handlerResolver = new MockRequestHandlerResolver(mockHandlerGenerator);
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var commandId = Guid.NewGuid().ToString();
            var command = new MockCommand(commandId);
            var commandResponse = new MockCommandResult(true, commandId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPreProcessing<MockCommand, MockCommandResult>(command);
            #endregion Act

            #region Assert            
            mockHandlerGenerator.VerifyCommandPreProcessors(commandId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPreprocessing_ForCommandHandlers_WhenNoCommandProcessorsArePresent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _commandPreProcessors = null,
                _commandPostProcessors = null

            };
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var commandId = Guid.NewGuid().ToString();
            var command = new MockCommand(commandId);
            var commandResponse = new MockCommandResult(true, commandId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPreProcessing<MockCommand, MockCommandResult>(command);
            #endregion Act

            #region Assert            
            mockHandlerGenerator.VerifyGlobalPreProcessors(commandId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPostprocessing_ForCommandHandlers_WhenNoGlobalProcessorsArePresent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _globalPreprocessors = null,
                _globalPostProcessors = null

            };
            var handlerResolver = new MockRequestHandlerResolver(mockHandlerGenerator);
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var commandId = Guid.NewGuid().ToString();
            var command = new MockCommand(commandId);
            var commandResponse = new MockCommandResult(true, commandId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPostProcessing<MockCommand, MockCommandResult>(command, commandResponse);
            #endregion Act

            #region Assert            
            mockHandlerGenerator.VerifyCommandPostProcessors(commandId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPostprocessing_ForCommandHandlers_WhenZeroGlobalProcessorsArePresent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _globalPreprocessors = new List<IGlobalRequestPreProcessor>(),
                _globalPostProcessors = new List<IGlobalRequestPostProcessor>()

            };
            var handlerResolver = new MockRequestHandlerResolver(mockHandlerGenerator);
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var commandId = Guid.NewGuid().ToString();
            var command = new MockCommand(commandId);
            var commandResponse = new MockCommandResult(true, commandId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPostProcessing<MockCommand, MockCommandResult>(command, commandResponse);
            #endregion Act

            #region Assert            
            mockHandlerGenerator.VerifyCommandPostProcessors(commandId);
            #endregion Assert
        }

        [TestMethod]
        public async Task RequestProcessingManager_ShouldHandleAllPostprocessing_ForCommandHandlers_WhenNoCommandProcessorsArePresent()
        {
            #region Arrange
            var mockHandlerGenerator = new MockHandlerGenerator
            {
                _commandPreProcessors = null,
                _commandPostProcessors = null

            };
            var handlerResolver = new RequestHandlerResolver(mockHandlerGenerator.GetResolverFunc());
            var requestProcessingManager = new RequestProcessingManager(handlerResolver);
            var commandId = Guid.NewGuid().ToString();
            var command = new MockCommand(commandId);
            var commandResponse = new MockCommandResult(true, commandId);
            #endregion Arrange

            #region Act
            await requestProcessingManager.HandleRequestPostProcessing<MockCommand, MockCommandResult>(command, commandResponse);
            #endregion Act

            #region Assert            
            mockHandlerGenerator.VerifyGlobalPostProcessors(commandId);
            #endregion Assert
        }
        #endregion Command Processing
    }
}
