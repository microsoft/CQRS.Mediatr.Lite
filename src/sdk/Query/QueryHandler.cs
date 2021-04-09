namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Base handler for query requests
    /// </summary>
    /// <typeparam name="QueryRequest">Query Type</typeparam>
    /// <typeparam name="QueryResponse">Response Type</typeparam>
    public abstract class QueryHandler<QueryRequest, QueryResponse> :
        RequestHandler<QueryRequest, QueryResponse> where QueryRequest : Query<QueryResponse>
    {
        
    }
}
