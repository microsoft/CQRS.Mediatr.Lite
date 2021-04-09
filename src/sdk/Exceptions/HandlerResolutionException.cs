using System;

namespace CQRS.Mediatr.Lite.Exceptions
{
    /// <summary>
    /// Exception when there was an error in resolving a handler
    /// </summary>
    public class HandlerResolutionException: Exception
    {
        public HandlerResolutionException(Type handlerType, Exception exception)
            :base($"There was an error in creating the handler of type {handlerType.FullName}. Please check the dependency resolution module.", exception)
        {}
    }
}
