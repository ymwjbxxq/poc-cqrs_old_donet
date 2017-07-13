using System;

namespace CommonStuff.Queue
{
    public class MessageEvent
    {
        public string EventName { get; set; }

        public string EventData { get; set; }

        public Guid EventId { get; set; }
    }
}
