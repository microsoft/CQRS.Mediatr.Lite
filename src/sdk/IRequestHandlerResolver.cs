using System.Collections.Generic;

namespace CQRS.Mediatr.Lite
{
    /// <summary>
    /// Base interface for resolving a Request Handler
    /// </summary>
    public interface IRequestHandlerResolver
    {
        T Resolve<T>();
        IEnumerable<T> ResolveAll<T>();
    }
}
