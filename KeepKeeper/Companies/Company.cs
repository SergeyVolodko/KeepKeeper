using System.Collections.Generic;
using KeepKeeper.Common;
using KeepKeeper.Companies.Events;

namespace KeepKeeper.Companies
{
    public class Company: AggregateRoot
    {
        public string Name { get; set; }
        
        public Company(IList<Event> events)
        {
            foreach (var @event in events)
            {
                ApplyChange(@event);
            }
        }

        private void Apply(CompanyCreated @event)
        {
            Id = @event.EntityId;
            Name = @event.Name;
        }
    }
}
