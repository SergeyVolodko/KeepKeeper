using KeepKeeper.Common;
using KeepKeeper.Companies;
using KeepKeeper.Companies.Events;
using KeepKeeper.Tests.Infrastructure;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace KeepKeeper.Tests.CompanyServiceTests
{
    public class ChangeName
    {
        [Theory]
        [AutoNSubstituteData]
        public void calls_event_factory_create(
            [Frozen]IEventFactory factory,
            CompanyService sut,
            ChangeCompanyNameData data)
        {
            // act
            sut.ChangeName(data);

            // assert
            factory.Received()
                .CreateCompanyRenamedEvent(data.NewName);
        }


        [Theory]
        [AutoNSubstituteData]
        public void calls_event_store_save(
            [Frozen]IEventRepository repo,
            [Frozen]IEventFactory factory,
            CompanyService sut,
            ChangeCompanyNameData data,
            CompanyRenamed @event)
        {
            // arrange
            factory
                .CreateCompanyRenamedEvent(data.NewName)
                .Returns(@event);

            // act
            sut.ChangeName(data);

            // assert
            repo.Received().Save(@event);
        }
    }
}
