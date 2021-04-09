using System;

namespace CQRS.Mediatr.Lite.Exceptions
{
    /// <summary>
    /// Exception when Handler is not found
    /// </summary>
    [Serializable]
    public class HandlerNotFoundException: Exception
    {
        public HandlerNotFoundException(Type handlerType)
            :base($"No handler of type {handlerType.FullName} was found. Please ensure that the handler has been registered in your dependency resolution.")
        { }

        public HandlerNotFoundException(string request)
            : base($"No handler for request {request} was found. Please ensure that the remote request has been registered.")
        { }
    }
}
