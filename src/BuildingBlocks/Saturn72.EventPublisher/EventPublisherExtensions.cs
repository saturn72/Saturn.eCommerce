using Saturn72.EventPublisher.Events;

namespace Saturn72.EventPublisher
{
    public static class EventPublisherExtensions
    {
        public static void PublishEntityCreatedEvent<TData>(this IEventPublisher eventPublisher, TData data)
        {
            BuildEventAndPublish(eventPublisher, data, CrudEventType.Created);
        }

        public static void PublishEntityReadEvent<TData>(this IEventPublisher eventPublisher, TData data)
        {
            BuildEventAndPublish(eventPublisher, data, CrudEventType.Read);
        }


        public static void PublishEntityUpdatedEvent<TData>(this IEventPublisher eventPublisher, TData data)
        {
            BuildEventAndPublish(eventPublisher, data, CrudEventType.Update);
        }

        public static void PublishEntityDeletedEvent<TData>(this IEventPublisher eventPublisher, TData data)
        {
            BuildEventAndPublish(eventPublisher, data, CrudEventType.Deleted);
        }

        private static void BuildEventAndPublish<TData>(IEventPublisher eventPublisher, TData data, CrudEventType eventType)
        {
            eventPublisher.Publish(new CrudEvent<TData>(eventType, data));
        }
    }
}