using KeepKeeper.Api.Contarcts;
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
			var company = Company.Create(
				command.CompanyName,
				command.VatNumber,
				command.Email,
				command.TenantId,
				DateTimeOffset.UtcNow);

			return store.Save(company);
		}

		public async Task Handle(CompanyCommands.V1.Rename command)
		{
			var company = await store.Load<Company>(command.CompanyId.ToString());
			company.Rename(command.NewName, DateTimeOffset.UtcNow);
			await store.Save(company);
		}

		/// <summary>
		/// Optimized
		/// </summary>


		public Task Handle(CompanyCommands.V1.ChangeVatNumber command)
			=> HandleUpdate(command.CompanyId, company =>
					company.ChangeVatNumber(
						command.NewVatNumber,
						DateTimeOffset.UtcNow));

		public Task Handle(CompanyCommands.V1.ChangeEmail command)
			=> HandleUpdate(command.CompanyId, company =>
					company.ChangeEmail(
						command.NewEmail,
						DateTimeOffset.UtcNow));


		public Task Handle(CompanyCommands.V1.AddAddress c)
			=> HandleUpdate(c.CompanyId, company =>
						company.AddAddress(
							new Address(c.AddressLine1, c.AddressLine2, c.AddressCity, c.AddressPostCode, c.AddressCountry),
							DateTimeOffset.UtcNow));

		public Task Handle(CompanyCommands.V1.ChangeAddress c)
			=> HandleUpdate(c.CompanyId, company =>
						company.ChangeAddress(
							new Address(c.AddressLine1, c.AddressLine2, c.AddressCity, c.AddressPostCode, c.AddressCountry),
							DateTimeOffset.UtcNow));

		public Task Handle(CompanyCommands.V1.RemoveAddress command)
			=> HandleUpdate(command.CompanyId, company =>
						company.RemoveAddress(
							DateTimeOffset.UtcNow));

		private async Task HandleUpdate(Guid id, Action<Company> update)
		{
			var company = await store.Load<Company>(id.ToString());
			update(company);
			await store.Save(company);
		}
	}
}
