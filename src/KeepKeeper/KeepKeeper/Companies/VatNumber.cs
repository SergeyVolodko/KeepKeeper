using KeepKeeper.Framework;
using System.Text.RegularExpressions;

namespace KeepKeeper.Companies
{
	public class VatNumber : Value<VatNumber>
	{
		public string Value { get; }

		public VatNumber(string value)
		{
			var vatRegex = new Regex("^NL[0-9]{9}B[0-9]{2}$");
			if (vatRegex.IsMatch(value))
			{
				throw new ValidationException("VatNumber", "invalid_vat");
			}

			Value = value;
		}

		public static implicit operator string(VatNumber self) => self.Value;

		public static implicit operator VatNumber(string value) => new VatNumber(value);
	}
}
