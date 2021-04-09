using System.Threading.Tasks;

namespace CQRS.Mediatr.Lite
{
    public interface IRemoteQueryService
    {
        Task<object> Query(string queryName, string serializedQuery);
    }
}