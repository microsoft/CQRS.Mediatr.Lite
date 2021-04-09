using System.Threading.Tasks;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Interface for the bus for sending commands
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// Sends a commands
        /// </summary>
        /// <typeparam name="TCommandResult">Type of response from executing the command</typeparam>
        /// <param name="command">Command passed to the handler</param>
        /// <returns cref="CommandResult">Result from executing the command</returns>
        Task<TCommandResult> Send<TCommandResult>(Command<TCommandResult> command) where TCommandResult: CommandResult;
    }
}
