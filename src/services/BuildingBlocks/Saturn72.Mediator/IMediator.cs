using System.Threading.Tasks;

namespace Saturn72.Mediator
{
    public interface IMediator
    {
        Task CommandAndForget<TData>(CommandRequest<TData> command);
        Task<CommandResponse<TData>> Command<TData>(CommandRequest<TData> command);

    }
}
