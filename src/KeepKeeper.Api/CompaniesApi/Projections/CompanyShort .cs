using KeepKeeper.Companies;
using KeepKeeper.Framework;
using Raven.Client.Documents.Session;
using System;
using System.Threading.Tasks;

namespace KeepKeeper.Api.CompaniesApi.Projections
{
	public class CompanyShort : Projection
	{
		private readonly Func<IAsyncDocumentSession> _openSession;

		public CompanyShort(Func<IAsyncDocumentSession> getSession) => _openSession = getSession;

		public override async Task Handle(object e)
		{
			ShortCompanyDocument doc;
			using (var session = _openSession())
			{
				switch (e)
				{
					case Events.V1.CompanyCreated x:
						doc = new ShortCompanyDocument
						{
							Id = DocumentId(x.Id),
							Name = x.Name,
							VatNumber = x.VatNumber,
						};
						await session.StoreAsync(doc);
						break;

					case Events.V1.CompanyRenamed x:
						doc = await session.LoadAsync<ShortCompanyDocument>(DocumentId(x.Id));
						doc.Name = x.Name;
						break;

					case Events.V1.CompanyVatNumberChanged x:
						doc = await session.LoadAsync<ShortCompanyDocument>(DocumentId(x.Id));
						doc.VatNumber = x.VatNumber;
						break;
				}
				await session.SaveChangesAsync();
			}
		}

		private static string DocumentId(Guid id) => $"ShortCompany/{id}";
	}

	public class ShortCompanyDocument
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string VatNumber { get; set; }
	}
}
