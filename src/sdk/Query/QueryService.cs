using System;
using System.Threading.Tasks;
using CQRS.Mediatr.Lite.Internal;
using System.Collections.Generic;

namespace CQRS.Mediatr.Lite
{   
    /// <summary>
    /// Single point of entry for serving all query requests
    /// </summary>
    public class QueryService : IQueryService
    {
        private readonly Dictionary<Type, object> _queryHandlers;
        private readonly IRequestHandlerResolver _handlerResolver;

        /// <summary>
        /// Constructs the query Service
        /// </summary>
        /// <param name="handlerResolver">Resolves Request Handler for every reqeust</param>
        public QueryService(IRequestHandlerResolver handlerResolver)
        {
            _queryHandlers = new Dictionary<Type, object>();
            _handlerResolver = handlerResolver;
        }
        
        /// <summary>
        /// Executes the Query
        /// </summary>
        /// <typeparam name="TResponse">Reponse Type of the query result</typeparam>
        /// <param name="query">Query Request</param>
        /// <returns>Response from executing the query</returns>
        public Task<TResponse> Query<TResponse>(Query<TResponse> query)
        {
            var queryType = query.GetType();
            
            if (!_queryHandlers.ContainsKey(queryType))
            {
                // Creates a Query Handler Wrapper
                var queryHandlerWrapper = Activator.CreateInstance(typeof(QueryHandlerWrapper<,>)
                    .MakeGenericType(queryType, typeof(TResponse)));
                _queryHandlers.Add(queryType, queryHandlerWrapper);
            }
            var queryHandler = (IQueryHandlerWrapper<TResponse>)_queryHandlers[queryType];
            return queryHandler.Handle(query, _handlerResolver);
        }
    }
}
