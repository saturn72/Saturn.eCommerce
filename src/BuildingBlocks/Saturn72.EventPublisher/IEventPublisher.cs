namespace Saturn72.EventPublisher
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event);
    }
}

