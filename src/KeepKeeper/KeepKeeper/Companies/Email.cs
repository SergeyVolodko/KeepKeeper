using KeepKeeper.Framework;
using System.Text.RegularExpressions;

namespace KeepKeeper.Companies
{
	public class Email : Value<Email>
	{
		public string Value { get; }

		public Email(string value)
		{
			var vatRegex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
			if (!vatRegex.IsMatch(value))
			{
				throw new ValidationException("Email", "invalid_email");
			}

			Value = value;
		}

		public static implicit operator string(Email self) => self.Value;

		public static implicit operator Email(string value) => new Email(value);
	}
}
