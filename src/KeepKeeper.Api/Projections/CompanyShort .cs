﻿using System;
using System.Threading.Tasks;
using KeepKeeper.Companies;
using KeepKeeper.Framework;
using Raven.Client.Documents.Session;

namespace KeepKeeper.Api.Projections
{
	public class CompanyShort : Projection
	{
		private readonly Func<IAsyncDocumentSession> _openSession;

		public CompanyShort(Func<IAsyncDocumentSession> getSession) => _openSession = getSession;

		public override async Task Handle(object e)
		{
			CompanyShortDocument doc;
			using (var session = _openSession())
			{
				switch (e)
				{
					case Events.V1.CompanyCreated x:
						doc = new CompanyShortDocument
						{
							Id = DocumentId(x.Id),
							Name = x.Name,
							Email = x.Email
						};
						await session.StoreAsync(doc);
						break;

					case Events.V1.CompanyRenamed x:
						doc = await session.LoadAsync<CompanyShortDocument>(DocumentId(x.Id));
						doc.Name = x.Name;
						break;

					case Events.V1.CompanyEmailChanged x:
						doc = await session.LoadAsync<CompanyShortDocument>(DocumentId(x.Id));
						doc.Email = x.Email;
						break;
				}
				await session.SaveChangesAsync();
			}
		}

		private static string DocumentId(Guid id) => $"{nameof(CompanyShort)}/{id}";
	}

	public class CompanyShortDocument
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
	}
}
