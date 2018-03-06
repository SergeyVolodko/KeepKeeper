using System.Threading;
using System.Threading.Tasks;

namespace KeepKeeper.Framework
{
	public class MyAggregateStoreMock : IAggregateStore
	{
		public Task<T> Load<T>(string aggregateId, CancellationToken cancellationToken = default) where T : Aggregate, new()
		{
			return Task.FromResult(new T());
		}

		public Task<(long NextExpectedVersion, long LogPosition, long CommitPosition)> Save<T>(T aggregate, CancellationToken cancellationToken = default) where T : Aggregate
		{
			return Task.FromResult<(long NextExpectedVersion, long LogPosition, long CommitPosition)>(default);
		}
	}
}
