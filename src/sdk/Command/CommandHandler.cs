namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Base handler for commands
    /// </summary>
    /// <typeparam name="TCommand">Command Type</typeparam>
    /// <typeparam name="TCommandResponse">Command Response</typeparam>
    public abstract class CommandHandler<TCommand, TCommandResponse>
        : RequestHandler<TCommand, TCommandResponse>
        where TCommandResponse : CommandResult
        where TCommand : Command<TCommandResponse>
    { }
}
