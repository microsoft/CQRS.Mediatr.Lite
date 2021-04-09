namespace CQRS.Mediatr.Lite
{
    public interface IRequest
    {
        string DisplayName { get; }
        string Id { get; }

        /// <summary>
        /// Validates the request object
        /// </summary>
        /// <returns>True - If query is valid. False - If query is invalid</returns>
        bool Validate(out string ValidationErrorMessage);
    }

    /// <summary>
    /// Represents a request which returns a response - Query/Command/Event
    /// </summary>
    /// <typeparam name="IResponse">Response type of the Request</typeparam>
    public interface IRequest<IResponse>: IRequest
    { }
}
