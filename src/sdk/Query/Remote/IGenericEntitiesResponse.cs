using System.Collections.Generic;

namespace CQRS.Mediatr.Lite
{
    public interface IGenericEntitiesResponse<TResponse>
    {
        int Count { get; set; }
        long TotalCount { get; set; }
        IList<TResponse> Results { get; set; }
        string Source { get; set; }
        string ParentSource { get; set; }
    }
}
