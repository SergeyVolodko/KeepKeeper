using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeepKeeper.Framework
{
	public interface IReadRepository
	{
		T Load<T>(string documentName, Guid id);
		
		IList<T> LoadMany<T>();
	}

	public class RavenReadRepository : IReadRepository
	{
		private readonly Func<IAsyncDocumentSession> openSession;

		public RavenReadRepository(Func<IAsyncDocumentSession> openSession)
		{
			this.openSession = openSession;
		}

		public T Load<T>(string documentName, Guid id)
		{
			using (var session = openSession())
			{
				var docId = $"{documentName}/{id}";
				return session.LoadAsync<T>(docId).Result;
			}
		}

		public IList<T> LoadMany<T>()
		{
			using (var session = openSession())
			{
				return session.Advanced
					.AsyncDocumentQuery<T>()
					.ToListAsync()
					.Result;
			}
		}
	}
}
