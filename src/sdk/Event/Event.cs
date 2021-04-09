using System;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Base representation for an Event
    /// </summary>
    public abstract class Event : IRequest
    {
        public abstract string DisplayName { get; }
        public abstract string Id { get; set; }

        public string CorrelationId { get; set; }
        public string TransactionId { get; set; }
        public string GeneratedBy { get; set; }
        public DateTime GeneratedOn { get; set; } 
        public string Version { get; set; }

        public Event() { }

        /// <summary>
        /// Validates an event.
        /// </summary>
        /// <param name="ValidationErrorMessage">Error Message, if any</param>
        /// <returns>True - If event is valid</returns>
        /// <remarks>Ideally therey is no need for events to have a validation</remarks>
        public virtual bool Validate(out string ValidationErrorMessage)
        {
            // Events in general doesn't need to be validated
            ValidationErrorMessage = null;
            return true;
        }
    }
}
