using KeepKeeper.Framework;

namespace KeepKeeper.Companies
{
	public class Name : Value<Name>
	{
		public string Value { get; }

		public Name(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ValidationException("Name", "notempty_error");
			}

			Value = value;
		}

		public static implicit operator string(Name self) => self.Value;

		public static implicit operator Name(string value) => new Name(value);
	}
}
