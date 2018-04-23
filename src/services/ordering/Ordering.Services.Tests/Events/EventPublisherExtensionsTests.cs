using System;
using Moq;
using Ordering.Services.Events;
using Xunit;

namespace Ordering.Services.Tests.Events
{
    public class EventPublisherExtensionsTests
    {
        [Theory]
        [InlineData(EventType.Created, typeof(CreatedEvent))]
        public void EventPublisherExtensions_Publish(EventType eventType, Type expEventType)
        {
            var ep = new Mock<IEventPublisher>();

            var data = "some-data";
            EventPublisherExtensions.PublishAsync(ep.Object, eventType, data);

            ep.Verify(e=>e.Publish(It.Is<EventBase>(ev =>
            ev.GetType() == expEventType && ev.Data == data && ev.CreatedOnUtc > default(DateTime))), Times.Once);
        }
    }
}
