using System.Threading.Tasks;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Base interface any request pre-processor
    /// </summary>
    public interface IRequestPreProcessor<in TRequest>
    {
        /// <summary>
        /// Process the request before the handler the can recive it
        /// </summary>
        /// <param name="request">Incoming request</param>
        /// <returns>Completed Task</returns>
        Task Process(TRequest request);
    }
}
