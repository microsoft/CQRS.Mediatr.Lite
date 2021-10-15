using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite
{   
    [ExcludeFromCodeCoverage]
    public abstract class GenericEntitiesQuery<QueryResponse> : Query<GenericEntitiesQueryResponse<QueryResponse>>, IGenericSearchQuery
    {
        public string SearchText { get; set; }
        public string Filter { get; set; }
        public string Select { get; set; }
        public string OrderBy { get; set; }
        public int Top { get; set; }
        public int Skip { get; set; }
        public bool IncludeTotalCount { get; set; }
    }
}
