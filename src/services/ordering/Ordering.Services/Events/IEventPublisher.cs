using System;
using System.Collections.Generic;

namespace Ordering.Services.Events
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event);
    }

    public static class EventPublisherExtensions
    {
        private static readonly IDictionary<EventType, Func<object, EventBase>> EventTypeDictionary =
            new Dictionary<EventType, Func<object, EventBase>>
            {
                {EventType.Created, data => new CreatedEvent(data)},
            };

        public static void  PublishAsync<TData>(this IEventPublisher eventPublisher, EventType eventType, TData data)
        {
            var @event = EventTypeDictionary[eventType](data);
            eventPublisher.Publish(@event);
        }
    }
}

