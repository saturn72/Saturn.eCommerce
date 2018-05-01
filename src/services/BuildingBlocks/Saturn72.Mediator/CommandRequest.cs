namespace Saturn72.Mediator
{
    public class CommandRequest<TData>
    {
        public CommandRequest(TData data)
        {
            Data = data;
        }

        public TData Data { get;}
    }
}