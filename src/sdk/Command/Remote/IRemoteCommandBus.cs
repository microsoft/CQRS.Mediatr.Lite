using System.Threading.Tasks;

namespace CQRS.Mediatr.Lite
{
    public interface IRemoteCommandBus
    {
        Task<object> Send(string commandName, string serializedCommand);
    }
}