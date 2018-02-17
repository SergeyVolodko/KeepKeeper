using System;

namespace KeepKeeper
{
	public class ValidationException : Exception
	{
		public ValidationException(
			string propertyName,
			string code)
			: base($"Invalid value for '{propertyName}' Error: '{code}'")
		{
		}
	}
}
