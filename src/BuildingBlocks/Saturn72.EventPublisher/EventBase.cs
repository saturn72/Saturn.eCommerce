using System;

namespace Saturn72.EventPublisher
{
    public abstract class EventBase
    {
        protected EventBase(object data)
        {
            CreatedOnUtc = DateTime.UtcNow;
            Data = data;
        }

        public object Data { get;  }

        public DateTime CreatedOnUtc { get;  }
        public DateTime FiredOnUtc { get; set; }
    }
}