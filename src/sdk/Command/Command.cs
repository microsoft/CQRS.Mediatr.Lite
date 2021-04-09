using System;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Base representation of a Command
    /// </summary>
    /// <typeparam name="TCommandResponse">Type of the command response</typeparam>
    public abstract class Command<TCommandResponse> : IRequest<TCommandResponse> 
        where TCommandResponse : CommandResult
    {   
        public abstract string DisplayName { get; }
        public abstract string Id { get; }

        public string CorrelationId { get; set; }
        public string TransactionId { get; set; }
        public string Domain { get; set; }
        public DateTime IssuedAt { get; set; }

        public abstract bool Validate(out string ValidationErrorMessage);
    }
}
