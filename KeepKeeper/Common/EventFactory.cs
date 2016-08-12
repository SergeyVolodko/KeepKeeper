using System;
using KeepKeeper.Companies.Events;

namespace KeepKeeper.Common
{
    public interface IEventFactory
    {
        CompanyCreated CreateCompanyCreatedEvent(Guid companyId, string name);
        CompanyRenamed CreateCompanyRenamedEvent(string newName);
    }

    public class EventFactory: IEventFactory
    {
        public CompanyCreated CreateCompanyCreatedEvent(Guid companyId, string name)
        {
            var id = Guid.NewGuid();
            var timestamp = DateTime.UtcNow;

            return new CompanyCreated
            {
                Id = id,
                Timestamp = timestamp,
                EntityId = companyId,
                Name = name
            };
        }

        public CompanyRenamed CreateCompanyRenamedEvent(string newName)
        {
            throw new NotImplementedException();
        }
    }
}
