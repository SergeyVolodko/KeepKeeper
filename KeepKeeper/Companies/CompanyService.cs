using System.Data.Common;
using KeepKeeper.Common;

namespace KeepKeeper.Companies
{
    public interface ICompanyService
    {
        Company CreateCompany(CreateCompanyData createData);

        Company ChangeName(ChangeCompanyNameData data);
    }

    public class CompanyService: ICompanyService
    {
        private readonly IEventFactory factory;
        private readonly IEventRepository eventRepository;

        public CompanyService(
            IEventFactory factory,
            IEventRepository eventRepository)
        {
            this.factory = factory;
            this.eventRepository = eventRepository;
        }

        public Company CreateCompany(CreateCompanyData createData)
        {
            var @event = factory.CreateCompanyCreatedEvent(
                createData.CompanyId,
                createData.Name);

            eventRepository.Save(@event);

            var events = eventRepository.GetAllEventsForEntity(createData.CompanyId);

            return new Company(events);
        }

        public Company ChangeName(ChangeCompanyNameData renameData)
        {
            var @event = factory.CreateCompanyRenamedEvent(
                renameData.NewName);

            return null;
        }
    }
}
