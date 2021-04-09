using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Exceptions;

namespace CQRS.Mediatr.Lite.Internal
{
    /// <summary>
    /// Internal implementation for wrapping a Command Handler when executing in Remote mode
    /// </summary>
    /// <typeparam name="TCommand">Type of the command</typeparam>
    /// <typeparam name="TCommandResult">Type of the response received from executing the command</typeparam>
    internal class RemoteCommandHandlerWrapper<TCommand, TCommandResult> : IRemoteCommandHandlerWrapper
        where TCommandResult : CommandResult
        where TCommand : Command<TCommandResult>
    {
        public async Task<object> Handle(object command, IRequestHandlerResolver requestHandlerResolver)
        {
            var handler = requestHandlerResolver.Resolve<CommandHandler<TCommand, TCommandResult>>();
            if (handler == null)
                throw new HandlerNotFoundException(typeof(CommandHandler<TCommand, TCommandResult>));

            var processingManager = new RequestProcessingManager(requestHandlerResolver);

            await processingManager.HandleRequestPreProcessing<TCommand, TCommandResult>((TCommand)command);
            TCommandResult commandResult = await handler.Handle((TCommand)command);
            await processingManager.HandleRequestPostProcessing((TCommand)command, commandResult);
            return commandResult;
        }
    }

    internal interface IRemoteCommandHandlerWrapper
    {
        Task<object> Handle(object command, IRequestHandlerResolver requestHandlerResolver);
    }
}
