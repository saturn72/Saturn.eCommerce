using System.Threading.Tasks;

namespace Saturn72.Mediator.MediatR
{
    public class MediatRCommandMediator:IMediator
    {
        public Task CommandAndForget<TData>(CommandRequest<TData> command)
        {
            throw new System.NotImplementedException();
        }

        Task<CommandResponse<TData>> IMediator.Command<TData>(CommandRequest<TData> command)
        {
            throw new System.NotImplementedException();
        }
    }
}
