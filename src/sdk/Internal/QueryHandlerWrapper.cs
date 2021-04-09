using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Exceptions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CQRS.Mediatr.Lite.Tests")]
namespace CQRS.Mediatr.Lite.Internal
{
    /// <summary>
    /// Internal implementation for wrapping a Query Handler
    /// </summary>
    /// <typeparam name="TResponse">Type of response of the query</typeparam>
    internal interface IQueryHandlerWrapper<TResponse>
    {
        Task<TResponse> Handle(Query<TResponse> request, IRequestHandlerResolver requestHandlerResolver);
    }

    /// <summary>
    /// Internal implementation for wrapping a Query Handler
    /// </summary>
    /// <typeparam name="TQuery">Type of the query</typeparam>
    /// <typeparam name="TResponse">Type of response of the query</typeparam>
    internal class QueryHandlerWrapper<TQuery, TResponse>: IQueryHandlerWrapper<TResponse> where TQuery: Query<TResponse>
    {   
        public async Task<TResponse> Handle(Query<TResponse> request, IRequestHandlerResolver requestHandlerResolver)
        {
            var handler = requestHandlerResolver.Resolve<QueryHandler<TQuery, TResponse>>();
            if (handler == null)
                throw new HandlerNotFoundException(typeof(QueryHandler<TQuery, TResponse>));

            var processingManager = new RequestProcessingManager(requestHandlerResolver);

            await processingManager.HandleRequestPreProcessing<TQuery, TResponse>((TQuery)request);
            var result = await handler.Handle((TQuery)request);
            await processingManager.HandleRequestPostProcessing((TQuery)request, result);
            return result;
        }
    }
}
