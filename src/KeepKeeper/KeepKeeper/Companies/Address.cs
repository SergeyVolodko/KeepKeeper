using KeepKeeper.Framework;

namespace KeepKeeper.Companies
{
	public class Address : Value<Address>
	{
		public string Line1 { get; }
		public string Line2 { get; }
		public string City { get; }
		public string PostCode { get; }
		public string Country { get; }

		public Address(string line1, string line2, string city, string postCode, string country)
		{
			Line1 = line1;
			Line2 = line2;
			City = city;
			PostCode = postCode;
			Country = country;
		}
	}
}
