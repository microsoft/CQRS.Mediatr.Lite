using System;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Internal;
using System.Collections.Generic;
using CQRS.Mediatr.Lite.Exceptions;

namespace CQRS.Mediatr.Lite
{
    public class RemoteQueryService : IRemoteQueryService
    {
        private readonly Dictionary<Type, object> _queryHandlers;
        private readonly IRequestHandlerResolver _handlerResolver;
        private readonly IDictionary<string, Tuple<Type, Type>> _queries;

        /// <summary>
        /// Creates an instance of <see cref="RemoteQueryService">Remove Query Service</see>
        /// </summary>
        /// <param name="handlerResolver">Resolves Request Handler for every reqeust</param>
        public RemoteQueryService(IRequestHandlerResolver handlerResolver, IDictionary<string, Tuple<Type, Type>> queries)
        {
            _queryHandlers = new Dictionary<Type, object>();
            _handlerResolver = handlerResolver;
            _queries = queries?.ToDictionary(pair => pair.Key.ToLowerInvariant(), pair => pair.Value);
        }

        public Task<object> Query(string queryName, string serializedQuery)
        {
            if (!_queries.ContainsKey(queryName.ToLowerInvariant()))
                throw new HandlerNotFoundException(queryName.ToLowerInvariant());

            var queryType = _queries[queryName.ToLowerInvariant()].Item1;
            var queryResponseType = _queries[queryName.ToLowerInvariant()].Item2;

            if (!_queryHandlers.ContainsKey(queryType))
            {
                var queryHandlerWrapper = Activator.CreateInstance(typeof(RemoteQueryHandlerWrapper<,>)
                    .MakeGenericType(queryType, queryResponseType));
                _queryHandlers.Add(queryType, queryHandlerWrapper);
            }
            var queryHandler = _queryHandlers[queryType] as IRemoteQueryHandlerWrapper;
            var query = JsonConvert.DeserializeObject(serializedQuery, queryType);
            return queryHandler.Handle(query, _handlerResolver);
        }
    }
}
