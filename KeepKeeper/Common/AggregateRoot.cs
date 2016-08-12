using System;
using KeepKeeper.Common.Infrastructure;

namespace KeepKeeper.Common
{
    public class AggregateRoot
    {
        public Guid Id { get; set; }

        // it is seems to be a visitor implementation
        protected void ApplyChange(Event @event)
        {
            this.AsDynamic().Apply(@event);
        }
    }
}
