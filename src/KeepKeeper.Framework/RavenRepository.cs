using Raven.Client.Documents.Session;
using System;

namespace KeepKeeper.Framework
{
	public interface IReadRepository
	{
		T Load<T>(string documentName, Guid id);
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
	}
}
