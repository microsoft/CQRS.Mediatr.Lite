using System.Threading.Tasks;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Base interface for post-processing a request
    /// </summary>
    /// <typeparam name="TRequest">Type of the Request received by the handler</typeparam>
    /// <typeparam name="TResponse">Type of the Response sent by the handler</typeparam>
    public interface IRequestPostProcessor<in TRequest, in TResponse>
    {
        /// <summary>
        /// Processes the request and response after handler has completed execution
        /// </summary>
        /// <param name="request">Request received by the handler</param>
        /// <param name="response">Response sent by the handler</param>
        /// <returns>Completed Task</returns>
        Task Process(TRequest request, TResponse response);
    }
}
