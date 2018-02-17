using KeepKeeper.Framework;
using System;

namespace KeepKeeper.Companies
{
	public class CompanyId : Value<CompanyId>
	{
		private readonly Guid _value;

		public CompanyId(Guid value)
		{
			_value = value;
		}

		public static implicit operator Guid(CompanyId self) => self._value;

		public static implicit operator CompanyId(Guid value) => new CompanyId(value);
	}
}
