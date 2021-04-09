using System.Threading.Tasks;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Interface for the Query Service
    /// </summary>
    public interface IQueryService
    {
        /// <summary>
        /// Executes the query
        /// </summary>
        /// <typeparam name="TResponse">Type of ther query response</typeparam>
        /// <param name="query">Query request</param>
        /// <returns>Query Response</returns>
        Task<TResponse> Query<TResponse>(Query<TResponse> query);
    }
}
