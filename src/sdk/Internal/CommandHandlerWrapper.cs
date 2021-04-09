using CQRS.Mediatr.Lite.Exceptions;
using System.Threading.Tasks;

namespace CQRS.Mediatr.Lite.Internal
{
    internal interface ICommandHandlerWrapper<TCommandResult> where TCommandResult: CommandResult
    {
        Task<TCommandResult> Handle(Command<TCommandResult> command, IRequestHandlerResolver requestHandlerResolver);
    }

    /// <summary>
    /// Internal implementation for wrapping a Command Handler
    /// </summary>
    /// <typeparam name="TCommand">Type of the command</typeparam>
    /// <typeparam name="TCommandResult">Type of the response received from executing the command</typeparam>
    internal class CommandHandlerWrapper<TCommand, TCommandResult> : ICommandHandlerWrapper<TCommandResult> 
        where TCommandResult : CommandResult
        where TCommand: Command<TCommandResult>
    {
        public async Task<TCommandResult> Handle(Command<TCommandResult> command, IRequestHandlerResolver requestHandlerResolver)
        {   
            var handler = requestHandlerResolver.Resolve<CommandHandler<TCommand, TCommandResult>>();
            if (handler == null)
                throw new HandlerNotFoundException(typeof(CommandHandler<TCommand, TCommandResult>));

            var processingManager = new RequestProcessingManager(requestHandlerResolver);

            await processingManager.HandleRequestPreProcessing<TCommand, TCommandResult>((TCommand)command);
            var commandResult = await handler.Handle((TCommand)command);
            await processingManager.HandleRequestPostProcessing((TCommand)command, commandResult);
            return commandResult;
        }
    }
}
