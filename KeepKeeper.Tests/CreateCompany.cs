using System;
using System.Collections.Generic;
using FluentAssertions;
using KeepKeeper.Common;
using KeepKeeper.Companies;
using KeepKeeper.Companies.Events;
using KeepKeeper.Tests.Infrastructure;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace KeepKeeper.Tests
{
    public class CreateCompany
    {
        [Theory]
        [AutoNSubstituteData]
        public void company_service_create_calls_event_factory_create(
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
        public void event_factory_create_company_created_event_returns_proper_event(
            EventFactory sut,
            Guid companyId, 
            string name)
        {
            // arrange
            var expected = new CompanyCreated { EntityId = companyId, Name = name };

            // act
            var actual = sut.CreateCompanyCreatedEvent(companyId, name);

            // assert
            actual.Id.Should().NotBeEmpty();
            actual.Timestamp.Should().BeCloseTo(DateTime.UtcNow);
            
            actual.ShouldBeEquivalentTo(expected, options => options
                    .Excluding(o => o.Id)
                    .Excluding(o => o.Timestamp));
        }

        [Theory]
        [AutoNSubstituteData]
        public void compny_service_create_calls_event_store_save(
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
        public void compny_service_create_calls_event_get_all_events_by_entity_id(
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
        public void compny_service_create_returns_new_company(
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
