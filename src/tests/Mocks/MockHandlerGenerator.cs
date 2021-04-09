using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockHandlerGenerator
    {
        public List<IGlobalRequestPreProcessor> _globalPreprocessors;
        public List<IGlobalRequestPostProcessor> _globalPostProcessors;

        public MockQueryHandler _queryHandler;
        public List<IRequestPreProcessor<MockQuery>> _queryPreProcessors;
        public List<IRequestPostProcessor<MockQuery, MockQueryResponse>> _queryPostProcessors;

        public MockCommandHandler _commandHandler;
        public List<IRequestPreProcessor<MockCommand>> _commandPreProcessors;
        public List<IRequestPostProcessor<MockCommand, MockCommandResult>> _commandPostProcessors;

        public List<EventHandler<MockEvent>> _eventHandlers;

        public MockHandlerGenerator()
        {
            _globalPreprocessors = new List<IGlobalRequestPreProcessor>() { new MockGlobalRequestPreProcessor() };
            _globalPostProcessors = new List<IGlobalRequestPostProcessor>() { new MockGlobalRequestPostProcessor() };

            _queryHandler = new MockQueryHandler();
            _queryPreProcessors = new List<IRequestPreProcessor<MockQuery>> { new MockQueryPreProcessor() };
            _queryPostProcessors = new List<IRequestPostProcessor<MockQuery, MockQueryResponse>> { new MockQueryPostProcessor() };

            _commandHandler = new MockCommandHandler();
            _commandPreProcessors = new List<IRequestPreProcessor<MockCommand>>() { new MockCommandPreProcessor() };
            _commandPostProcessors = new List<IRequestPostProcessor<MockCommand, MockCommandResult>>() { new MockCommandPostProcessor() };

            _eventHandlers = new List<EventHandler<MockEvent>>() { new MockEventHandler(), new MockEventHandler2() };
        }

        public Func<Type, object> GetResolverFunc()
        {
            object resolver(Type type)
            {
                return Resolve(type);
            }
            return resolver;
        }

        public object Resolve(Type type)
        {
            // Global Processors
            if (type == typeof(IEnumerable<IGlobalRequestPreProcessor>))
                return _globalPreprocessors;

            if (type == typeof(IEnumerable<IGlobalRequestPostProcessor>))
                return (_globalPostProcessors);

            //Query
            if (type == typeof(QueryHandler<MockQuery, MockQueryResponse>))
                return _queryHandler;

            if (type == typeof(IEnumerable<IRequestPreProcessor<MockQuery>>))
                return (_queryPreProcessors);

            if (type == typeof(IEnumerable<IRequestPostProcessor<MockQuery, MockQueryResponse>>))
                return (_queryPostProcessors);

            //Command
            if (type == typeof(CommandHandler<MockCommand, MockCommandResult>))
                return _commandHandler;

            if (type == typeof(IEnumerable<IRequestPreProcessor<MockCommand>>))
                return (_commandPreProcessors);

            if (type == typeof(IEnumerable<IRequestPostProcessor<MockCommand, MockCommandResult>>))
                return (_commandPostProcessors);

            //Event
            if (type == typeof(IEnumerable<EventHandler<MockEvent>>))
                return _eventHandlers;

            return null;
        }

        public void VerifyGlobalProcessors(string requestId)
        {
            VerifyGlobalPreProcessors(requestId);
            VerifyGlobalPostProcessors(requestId);
        }

        public void VerifyGlobalPreProcessors(string requestId)
        {
            if (!(_globalPreprocessors.First() is MockGlobalRequestPreProcessor globalPreProcessor) || !globalPreProcessor.IsGlobalPreProcessorCalled)
                throw new Exception("Global Preprocessor should have been called but wasn't called");
            var request = globalPreProcessor.Request;
            if (request == null || request.Id != requestId)
                throw new Exception("Request ID in the global pre-processor did not match.");
        }

        public void VerifyGlobalPostProcessors(string requestId)
        {
            if (!(_globalPostProcessors.First() is MockGlobalRequestPostProcessor globalPostProcessor) || !globalPostProcessor.IsGlobalProcessorCalled)
                throw new Exception("Global Preprocessor should have been called but wasn't called");

            var request = globalPostProcessor.Request;
            if (request == null || request.Id != requestId)
                throw new Exception("Request ID in the global post-processor did not match");

            if (globalPostProcessor.Response is MockQueryResponse queryResponse)
                if (queryResponse.RequestId != requestId)
                    throw new Exception("Request ID in the global post-processor response did not match");

            if (globalPostProcessor.Response is MockCommandResult commandResponse)
                if (commandResponse.CommandId != requestId)
                    throw new Exception("Command ID in the global post-processor response did not match");

        }

        public void VerifyQueryProcessors(string requestId)
        {
            VerifyQueryPreProcessors(requestId);
            VerifyQueryPostProcessors(requestId);
        }

        public void VerifyQueryPreProcessors(string requestId)
        {
            if (!(_queryPreProcessors.First() is MockQueryPreProcessor requestPreProcessor) || !requestPreProcessor.IsQueryPreProcessorCalled)
                throw new Exception("Mock Request Preprocessor should have been called but wasn't called");
            var request = requestPreProcessor.Query;
            if (request == null || request.Id != requestId)
                throw new Exception("Request ID in the request pre-processor did not match.");
        }

        public void VerifyQueryPostProcessors(string requestId)
        {
            if (!(_queryPostProcessors.First() is MockQueryPostProcessor requestPostProcessor) || !requestPostProcessor.IsQueryPostProcessorCalled)
                throw new Exception("Mock Request Preprocessor should have been called but wasn't called");

            var request = requestPostProcessor.Query;
            if (request == null || request.Id != requestId)
                throw new Exception("Request ID in the request post-processor did not match");

            var respone = requestPostProcessor.Response;
            if (respone == null || respone.RequestId != requestId)
                throw new Exception("Request ID in the request post-processor response did not match");
        }

        public void VerifyCommandProcessors(string requestId)
        {
            VerifyCommandPreProcessors(requestId);
            VerifyCommandPostProcessors(requestId);
        }

        public void VerifyCommandPreProcessors(string requestId)
        {
            if (!(_commandPreProcessors.First() is MockCommandPreProcessor requestPreProcessor) || !requestPreProcessor.IsCommandPreProcessorCalled)
                throw new Exception("Mock Request Preprocessor should have been called but wasn't called");
            var request = requestPreProcessor.Command;
            if (request == null || request.Id != requestId)
                throw new Exception("Request ID in the request pre-processor did not match.");
        }

        public void VerifyCommandPostProcessors(string requestId)
        {
            if (!(_commandPostProcessors.First() is MockCommandPostProcessor requestPostProcessor) || !requestPostProcessor.IsCommandPostProcessorCalled)
                throw new Exception("Mock Request Preprocessor should have been called but wasn't called");

            var request = requestPostProcessor.Command;
            if (request == null || request.Id != requestId)
                throw new Exception("Request ID in the request post-processor did not match");

            var respone = requestPostProcessor.CommandResult;
            if (respone == null || respone.CommandId != requestId)
                throw new Exception("Request ID in the request post-processor response did not match");
        }

        public void VerifyEventHandlers(string eventId)
        {
            _eventHandlers.ForEach(eventHandler =>
            {
                if (eventHandler is MockEventHandler)
                {
                    if (!((eventHandler as MockEventHandler).IsEventHandlerTriggered))
                        throw new Exception("Event Handler not triggered");

                    if ((eventHandler as MockEventHandler).EventId != eventId)
                        throw new Exception("Event ID sent to the handler is not matching");
                }
            });
        }
    }
}
