using CQRS.Mediatr.Lite;
using CQRS.Mediatr.Lite.Exceptions;
using CQRS.Mediatr.Lite.Internal;
using System.Threading.Tasks;

namespace CQRS.Mediatr.Lite.Internal
{
    internal class RemoteQueryHandlerWrapper<TQuery, TResponse> : IRemoteQueryHandlerWrapper where TQuery : Query<TResponse>
    {
        public async Task<object> Handle(object request, IRequestHandlerResolver requestHandlerResolver)
        {
            QueryHandler<TQuery, TResponse> handler = requestHandlerResolver.Resolve<QueryHandler<TQuery, TResponse>>();
            if (handler == null)
            {
                throw new HandlerNotFoundException(typeof(QueryHandler<TQuery, TResponse>));
            }
            RequestProcessingManager processingManager = new RequestProcessingManager(requestHandlerResolver);
            await processingManager.HandleRequestPreProcessing<TQuery, TResponse>((TQuery)request);
            TResponse result = await handler.Handle((TQuery)request);
            await processingManager.HandleRequestPostProcessing((TQuery)request, result);
            return result;
        }
    }

    internal interface IRemoteQueryHandlerWrapper
    {
        Task<object> Handle(object request, IRequestHandlerResolver requestHandlerResolver);
    }
}