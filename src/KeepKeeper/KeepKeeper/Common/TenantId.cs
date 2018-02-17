using KeepKeeper.Framework;
using System;

namespace KeepKeeper.Common
{
    public class TenantId : Value<TenantId>
	{
		private readonly Guid _value;

		public TenantId(Guid value)
		{
			_value = value;
		}

		public static implicit operator Guid(TenantId self) => self._value;

		public static implicit operator TenantId(Guid value) => new TenantId(value);
	}
}
