using System.Collections.Generic;
using FluentAssertions;
using KeepKeeper.Common;
using KeepKeeper.Companies;
using KeepKeeper.Companies.Events;
using KeepKeeper.Tests.Infrastructure;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace KeepKeeper.Tests.CompanyServiceTests
{
    public class CreateCompany
    {
        [Theory]
        [AutoNSubstituteData]
        public void calls_event_factory_create(
            [Frozen]IEventFactory factory, 
            CompanyService sut,
            CreateCompanyData data)
        {
            // act
            sut.CreateCompany(data);

            // assert
            factory.Received()
                .CreateCompanyCreatedEvent(data.CompanyId, data.Name);
        }
        
        [Theory]
        [AutoNSubstituteData]
        public void calls_event_store_save(
            [Frozen]IEventRepository repo,
            [Frozen]IEventFactory factory,
            CompanyService sut,
            CreateCompanyData data,
            CompanyCreated @event)
        {
            // arrange
            factory
                .CreateCompanyCreatedEvent(data.CompanyId, data.Name)
                .Returns(@event);

            // act
            sut.CreateCompany(data);

            // assert
            repo.Received().Save(@event);
        }

        [Theory]
        [AutoNSubstituteData]
        public void calls_event_get_all_events_by_entity_id(
            [Frozen]IEventRepository repo,
            [Frozen]IEventFactory factory,
            CompanyService sut,
            CreateCompanyData data)
        {
            // act
            sut.CreateCompany(data);

            // assert
            repo.Received().GetAllEventsForEntity(data.CompanyId);
        }

        [Theory]
        [AutoNSubstituteData]
        public void returns_new_company(
            [Frozen]IEventRepository repo,
            [Frozen]IEventFactory factory,
            CompanyService sut,
            CreateCompanyData data,
            CompanyCreated @event)
        {
            // arrange
            var events = new List<Event> { @event };
            repo.GetAllEventsForEntity(data.CompanyId)
                .Returns(events);

            var expected = new Company(events);

            // act // assert
            sut.CreateCompany(data)
                .ShouldBeEquivalentTo(expected);
        }

    }
}
