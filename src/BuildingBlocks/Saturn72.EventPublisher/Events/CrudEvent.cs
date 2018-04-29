namespace Saturn72.EventPublisher.Events
{
    public class CrudEvent<TData> : EventBase
    {
        public CrudEvent(CrudEventType crudEventType, TData data) : base(data)
        {
            CrudEventType = crudEventType;
        }

        public CrudEventType CrudEventType { get; }
    }
}