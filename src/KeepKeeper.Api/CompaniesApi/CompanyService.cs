using KeepKeeper.Api.Contarcts;
using KeepKeeper.Companies;
using KeepKeeper.Framework;
using System.Threading.Tasks;

namespace KeepKeeper.Api.CompaniesApi
{
	public class CompanyService
	{
		private readonly IAggregateStore store;

		public CompanyService(IAggregateStore store)
		{
			this.store = store;
		}

		public Task Handle(CompanyCommands.V1.Create command)
		{
			var ad = Company.Create(
				command.CompanyName,
				command.VatNumber,
				command.OwnerId,
				command.CreatedAt);

			return store.Save(ad);
		}

		public async Task Handle(CompanyCommands.V1.Rename command)
		{
			var company = await store.Load<Company>(command.CompanyId.ToString());
			company.Rename(command.NewName, command.RenamedAt);
			await store.Save(company);
		}

		public async Task Handle(CompanyCommands.V1.ChangeVatNumber command)
		{
			var company = await store.Load<Company>(command.CompanyId.ToString());
			company.ChangeVatNumber(command.NewVatNumber, command.ChangedAt);
			await store.Save(company);
		}
	}
}
