using System;
using FluentAssertions;
using KeepKeeper.Common;
using KeepKeeper.Companies.Events;
using KeepKeeper.Tests.Infrastructure;
using Xunit;

namespace KeepKeeper.Tests
{
    public class EventsFactoryTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void create_company_created_event_returns_proper_event(
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

    }
}
