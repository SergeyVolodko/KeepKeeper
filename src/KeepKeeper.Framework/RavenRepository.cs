using Raven.Client.Documents.Session;
using System;

namespace KeepKeeper.Framework
{
	public interface IRepository
	{
		T Load<T>(string documentName, Guid id);
	}

	public class RavenRepository : IRepository
	{
		private readonly Func<IAsyncDocumentSession> openSession;

		public RavenRepository(Func<IAsyncDocumentSession> openSession)
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
