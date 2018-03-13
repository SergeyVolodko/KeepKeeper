using System;
using System.Threading.Tasks;
using KeepKeeper.Common;
using KeepKeeper.Companies;
using KeepKeeper.Framework;
using Raven.Client.Documents.Session;

namespace KeepKeeper.Api.Projections
{
	public class CompanyDetailed : Projection
	{
		private readonly Func<IAsyncDocumentSession> _openSession;

		public CompanyDetailed(Func<IAsyncDocumentSession> getSession) => _openSession = getSession;

		public override async Task Handle(object e)
		{
			CompanyDetailedDocument doc;
			using (var session = _openSession())
			{
				switch (e)
				{
					case Events.V1.CompanyCreated x:
						doc = new CompanyDetailedDocument
						{
							Id = DocumentId(x.Id),
							Name = x.Name,
							VatNumber = x.VatNumber,
							Email = x.Email
						};
						await session.StoreAsync(doc);
						break;

					case Events.V1.CompanyRenamed x:
						doc = await session.LoadAsync<CompanyDetailedDocument>(DocumentId(x.Id));
						doc.Name = x.Name;
						break;

					case Events.V1.CompanyVatNumberChanged x:
						doc = await session.LoadAsync<CompanyDetailedDocument>(DocumentId(x.Id));
						doc.VatNumber = x.VatNumber;
						break;

					case Events.V1.CompanyEmailChanged x:
						doc = await session.LoadAsync<CompanyDetailedDocument>(DocumentId(x.Id));
						doc.Email = x.Email;
						break;

					case Events.V1.CompanyAddressAdded x:
						doc = await session.LoadAsync<CompanyDetailedDocument>(DocumentId(x.Id));
						doc.Address = x.Address;
						break;
				}

				await session.SaveChangesAsync();
			}
		}

		private static string DocumentId(Guid id) => $"{nameof(CompanyDetailed)}/{id}";
	}

	public class CompanyDetailedDocument
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string VatNumber { get; set; }
		public Address Address { get; set; }
	}
}
