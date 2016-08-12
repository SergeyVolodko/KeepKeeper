using System;

namespace KeepKeeper.Common
{
    public class Event
    {
        public Guid Id { get; set; }

        public Guid EntityId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
