using KeepKeeper.Api.Contarcts;
using KeepKeeper.Common;
using KeepKeeper.Companies;
using KeepKeeper.Framework;
using System;
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

		/// <summary>
		/// Optimized
		/// </summary>


		public Task Handle(CompanyCommands.V1.ChangeVatNumber command)
			=> HandleUpdate(command.CompanyId, company =>
					company.ChangeVatNumber(command.NewVatNumber, command.ChangedAt));

		public Task Handle(CompanyCommands.V1.AddAddress c)
			=> HandleUpdate(c.CompanyId, company =>
						company.AddAddress(
							new Address(c.AddressLine1, c.AddressLine2, c.AddressCity, c.AddressPostCode, c.AddressCountry),
							c.AddedAt));

		public Task Handle(CompanyCommands.V1.ChangeAddress c)
			=> HandleUpdate(c.CompanyId, company =>
						company.ChangeAddress(
							new Address(c.AddressLine1, c.AddressLine2, c.AddressCity, c.AddressPostCode, c.AddressCountry),
							c.ChangedAt));

		public Task Handle(CompanyCommands.V1.RemoveAddress command)
			=> HandleUpdate(command.CompanyId, company =>
						company.RemoveAddress(command.RemovedAt));

		private async Task HandleUpdate(Guid id, Action<Company> update)
		{
			var company = await store.Load<Company>(id.ToString());
			update(company);
			await store.Save(company);
		}
	}
}
