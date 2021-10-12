using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Internal;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Bus for sending commands
    /// </summary>
    public class CommandBus : ICommandBus
    {
        private readonly IDictionary<Type, object> _commandHandlerWrappers;
        private readonly IRequestHandlerResolver _requestHandlerResolver;

        public CommandBus(IRequestHandlerResolver requestHandlerResolver)
        {
            _commandHandlerWrappers = new ConcurrentDictionary<Type, object>();
            _requestHandlerResolver = requestHandlerResolver;
        }

        /// <summary>
        /// Method for sending the commands
        /// </summary>
        /// <typeparam name="TCommandResult">Type of the response from executing the command</typeparam>
        /// <param name="command" cref="Command{CommandResult}">Command being sent for execution</param>
        /// <returns>Command Resut</returns>
        public Task<TCommandResult> Send<TCommandResult>(Command<TCommandResult> command) where TCommandResult : CommandResult
        {
            var commandType = command.GetType();
            if (!_commandHandlerWrappers.ContainsKey(commandType))
            {
                var commandHandlerWrapper = Activator.CreateInstance(typeof(CommandHandlerWrapper<,>)
                    .MakeGenericType(commandType, typeof(TCommandResult)));
                _commandHandlerWrappers.Add(commandType, commandHandlerWrapper);
            }
            var commandHandler = (ICommandHandlerWrapper<TCommandResult>)_commandHandlerWrappers[commandType];
            return commandHandler.Handle(command, _requestHandlerResolver);
        }
    }
}
