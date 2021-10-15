using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite
{
    [ExcludeFromCodeCoverage]
    public class GenericEntitiesQueryResponse<QueryResponse>: IGenericEntitiesResponse<QueryResponse>
    {
        public int Count { get; set; }
        public long TotalCount { get; set; }
        public IList<QueryResponse> Results { get; set; }
        public string Source { get; set; }
        public string ParentSource { get; set; }
    }
}
