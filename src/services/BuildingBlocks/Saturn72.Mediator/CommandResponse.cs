namespace Saturn72.Mediator
{
    public class CommandResponse<TData>
    {
        public CommandResponse(CommandRequest<TData> request)
        {
            Request = request;
        }

        public CommandRequest<TData> Request { get; }
    }
}