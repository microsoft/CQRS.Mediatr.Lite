using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Internal;
using CQRS.Mediatr.Lite.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Bus for sending commands
    /// </summary>
    public class RemoteCommandBus : IRemoteCommandBus
    {
        private readonly Dictionary<Type, object> _commandHandlerWrappers;
        private readonly IRequestHandlerResolver _requestHandlerResolver;
        private readonly IDictionary<string, Tuple<Type, Type>> _commands;

        public RemoteCommandBus(IRequestHandlerResolver requestHandlerResolver, IDictionary<string, Tuple<Type, Type>> commands)
        {
            _commandHandlerWrappers = new Dictionary<Type, object>();
            _requestHandlerResolver = requestHandlerResolver;
            _commands = commands?.ToDictionary(pair => pair.Key.ToLowerInvariant(), pair => pair.Value);
        }

        /// <summary>
        /// Method for sending the commands
        /// </summary>
        /// <typeparam name="TCommandResult">Type of the response from executing the command</typeparam>
        /// <param name="command" cref="Command{CommandResult}">Command being sent for execution</param>
        /// <returns>Command Resut</returns>
        public Task<object> Send(string commandName, string serializedCommand)
        {
            if (!_commands.ContainsKey(commandName.ToLowerInvariant()))
                throw new HandlerNotFoundException(commandName.ToLowerInvariant());

            var commandType = _commands[commandName.ToLowerInvariant()].Item1;
            var commandResponseType = _commands[commandName.ToLowerInvariant()].Item2;

            if (!_commandHandlerWrappers.ContainsKey(commandType))
            {
                var commandHandlerWrapper = Activator.CreateInstance(typeof(RemoteCommandHandlerWrapper<,>)
                    .MakeGenericType(commandType, commandResponseType));
                _commandHandlerWrappers.Add(commandType, commandHandlerWrapper);
            }
            var commandHandler = (IRemoteCommandHandlerWrapper)_commandHandlerWrappers[commandType];
            var command = JsonConvert.DeserializeObject(serializedCommand, commandType);
            return commandHandler.Handle(command, _requestHandlerResolver);
        }
    }
}
