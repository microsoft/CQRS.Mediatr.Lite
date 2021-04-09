namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Base representation of a query
    /// </summary>
    /// <typeparam name="QueryResponse">Type of the query response</typeparam>
    public abstract class Query<QueryResponse> : IRequest<QueryResponse>
    {
        public string CorrelationId { get; set; }
        public string TransactionId { get; set; }

        public abstract string DisplayName { get; }
        public abstract string Id { get; }

        public abstract bool Validate(out string ValidationErrorMessage);
        
    }
}
