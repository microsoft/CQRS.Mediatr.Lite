using System.Linq;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Exceptions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CQRS.Mediatr.Lite.Tests")]
namespace CQRS.Mediatr.Lite.Internal
{   
    /// <summary>
    /// Internal class for executing all the reqeust processors (pre/post)
    /// </summary>
    internal class RequestProcessingManager
    {
        private readonly IRequestHandlerResolver _handlerResolver;

        public RequestProcessingManager(IRequestHandlerResolver handlerResolver)
        {
            _handlerResolver = handlerResolver;
        }

        public async Task HandleRequestPreProcessing<Request, Response>(Request request)
            where Request : IRequest<Response>
        {
            await ExecuteGlobalPreProcessors(request);
            await ExecuteRequestPreProcessors<Request, Response>(request);
        }

        public async Task HandleRequestPostProcessing<Request, Response>(Request request, Response response)
            where Request : IRequest<Response>
        {
            await ExecuteGlobalPostProcessors(request, response);
            await ExecuteRequestPostProcessors<Request, Response>(request, response);
        }

        #region Pre-Processing
        private async Task ExecuteRequestPreProcessors<Request, Response>(Request request)
            where Request : IRequest<Response>
        {
            try
            {
                var preProcessors = _handlerResolver
                    .ResolveAll<IRequestPreProcessor<Request>>();

                if (preProcessors != null && preProcessors.Any())
                {
                    foreach (var preProcessor in preProcessors)
                    {
                        await preProcessor.Process(request);
                    }
                }
            }
            catch (HandlerNotFoundException)
            {
                //Empty global pre-processors are allowed
                return;
            }
        }

        private async Task ExecuteGlobalPreProcessors(IRequest request)
        {
            try
            {
                var globalPreRequestProcessors = _handlerResolver.ResolveAll<IGlobalRequestPreProcessor>();
                if (globalPreRequestProcessors == null || !globalPreRequestProcessors.Any())
                    return;

                foreach (var processor in globalPreRequestProcessors)
                {
                    await processor.Process(request);
                }
            }
            catch (HandlerNotFoundException)
            {
                //Empty global pre-processors are allowed
                return;
            }

        }
        #endregion Pre-Processing

        #region Post-Processing
        private async Task ExecuteRequestPostProcessors<TRequest, TResponse>(TRequest request, TResponse response)
            where TRequest: IRequest<TResponse>
        {
            try
            {
                var postProcessors = _handlerResolver
                    .ResolveAll<IRequestPostProcessor<TRequest, TResponse>>();

                if (postProcessors != null && postProcessors.Any())
                {
                    foreach (var postProcessor in postProcessors)
                    {
                        await postProcessor.Process(request, response);
                    }
                }
            }
            catch (HandlerNotFoundException)
            {
                //Empty global pre-processors are allowed
                return;
            }
        }

        private async Task ExecuteGlobalPostProcessors(IRequest request, object response)
        {
            try
            {
                var globalRequestPostProcessors = _handlerResolver.ResolveAll<IGlobalRequestPostProcessor>();
                if (globalRequestPostProcessors == null || !globalRequestPostProcessors.Any())
                    return;

                foreach (var processor in globalRequestPostProcessors)
                {
                    await processor.Process(request, response);
                }
            }
            catch (HandlerNotFoundException)
            {
                //Empty global post-processors are allowed
                return;
            }
        }
        #endregion Post-Processing
    }
}
