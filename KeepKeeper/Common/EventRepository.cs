using System;
using System.Collections;
using System.Collections.Generic;
using KeepKeeper.Companies.Events;

namespace KeepKeeper.Common
{
    public interface IEventRepository
    {
        void Save(dynamic @event);
        IList<Event> GetAllEventsForEntity(Guid entityId);
    }

    public class EventRepository: IEventRepository
    {
        public void Save(dynamic @event)
        {
            throw new NotImplementedException();
        }

        public IList<Event> GetAllEventsForEntity(Guid entityId)
        {
            throw new NotImplementedException();
        }
    }
}
