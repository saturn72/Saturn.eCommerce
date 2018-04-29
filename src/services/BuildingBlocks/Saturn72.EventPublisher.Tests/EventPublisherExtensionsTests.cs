using System;
using Moq;
using Saturn72.EventPublisher.Events;
using Xunit;

namespace Saturn72.EventPublisher.Tests
{
    public class EventPublisherExtensionsTests
    {
        [Fact]
        public void EventPublisherExtensions_PublishEntityCreatedEvent( )
        {
            var ep = new Mock<IEventPublisher>();

            var data = "some-data";
            EventPublisherExtensions.PublishEntityCreatedEvent(ep.Object, data);

            ep.Verify(e=>e.Publish(It.Is<CrudEvent<string>>(ev => ev.CrudEventType == CrudEventType.Created && ev.Data == data && ev.CreatedOnUtc > default(DateTime))), Times.Once);
        }

        [Fact]
        public void EventPublisherExtensions_PublishEntityReadEvent()
        {
            var ep = new Mock<IEventPublisher>();

            var data = "some-data";
            EventPublisherExtensions.PublishEntityReadEvent(ep.Object, data);

            ep.Verify(e => e.Publish(It.Is<CrudEvent<string>>(ev => ev.CrudEventType == CrudEventType.Read && ev.Data == data && ev.CreatedOnUtc > default(DateTime))), Times.Once);
        }

        [Fact]
        public void EventPublisherExtensions_PublishEntityUpdatedEvent()
        {
            var ep = new Mock<IEventPublisher>();

            var data = "some-data";
            EventPublisherExtensions.PublishEntityUpdatedEvent(ep.Object, data);

            ep.Verify(e => e.Publish(It.Is<CrudEvent<string>>(ev => ev.CrudEventType == CrudEventType.Update && ev.Data == data && ev.CreatedOnUtc > default(DateTime))), Times.Once);
        }

        [Fact]
        public void EventPublisherExtensions_PublishEntityDeletedEvent()
        {
            var ep = new Mock<IEventPublisher>();

            var data = "some-data";
            EventPublisherExtensions.PublishEntityDeletedEvent(ep.Object, data);

            ep.Verify(e => e.Publish(It.Is<CrudEvent<string>>(ev => ev.CrudEventType == CrudEventType.Deleted && ev.Data == data && ev.CreatedOnUtc > default(DateTime))), Times.Once);
        }
    }
}
