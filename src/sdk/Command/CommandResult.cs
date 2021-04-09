using System;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Represents a base for results  from executing Commands
    /// </summary>
    public abstract class CommandResult
    {
        /// <summary>
        /// Is the command executed sucessfully
        /// </summary>
        public bool IsSuccesfull { get; protected set; }

        public string Message { get; protected set; }

        /// <summary>
        /// When was the command executed
        /// </summary>
        public DateTime ExecutedAt { get; set; }

        /// <summary>
        /// How long did it take for process the commmand
        /// </summary>
        public TimeSpan TimeTaken { get; set; }


        public CommandResult(bool isSuccesfull, string message)
        {
            IsSuccesfull = isSuccesfull;
            Message = message;
        }

        public CommandResult(bool isSuccesfull)
            :this(isSuccesfull, null)
        { }
    }

    /// <summary>
    /// Represents a result where the result contains only the ID modified entity
    /// </summary>
    public class IdCommandResult: CommandResult
    {
        public string Id { get; private set; }

        public IdCommandResult(string id)
            :base(true)
        {
            Id = id;
        }

        public IdCommandResult(string id, string message)
            : base(true, message)
        {
            Id = id;
        }
    }
}
