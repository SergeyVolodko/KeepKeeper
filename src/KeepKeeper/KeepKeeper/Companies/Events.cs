using KeepKeeper.Common;
using System;

namespace KeepKeeper.Companies
{
    public static class Events
	{
		public static class V1
		{
			public class CompanyCreated
			{
				public Guid Id { get; set; }
				public Guid Owner { get; set; }
				public string Name { get; set; }
				public string VatNumber { get; set; }
				public DateTimeOffset CreatedAt { get; set; }

				public override string ToString()
					=> $"Company {Id} created.";
			}

			public class CompanyRenamed
			{
				public Guid Id { get; set; }
				public string Name { get; set; }
				public DateTimeOffset RenamedAt { get; set; }

				public override string ToString()
					=> $"Comapny {Id} renamed to '{(Name?.Length > 25 ? $"{Name?.Substring(0, 22)}..." : Name)}'";
			}

			public class CompanyVatNumberChanged
			{
				public Guid Id { get; set; }
				public string VatNumber { get; set; }
				public DateTimeOffset ChangedAt { get; set; }

				public override string ToString()
					=> $"Comapny {Id} VatNumber changed to '{VatNumber}'";
			}

			public class CompanyLogoAdded
			{
				public Guid Id { get; set; }
				public Picture Picture { get; set; }
				public DateTimeOffset AddedAt { get; set; }
			}

			public class CompanyLogoRemoved
			{
				public Guid Id { get; set; }
				public Picture Picture { get; set; }
				public DateTimeOffset RemovedAt { get; set; }
			}

			public class CompanyAddressAdded
			{
				public Guid Id { get; set; }
				public Address Address { get; set; }
				public DateTimeOffset AddedAt { get; set; }
			}

			public class CompanyAddressChanged
			{
				public Guid Id { get; set; }
				public Address Address { get; set; }
				public DateTimeOffset ChangeddAt { get; set; }
			}

			public class CompanyAddressRemoved
			{
				public Guid Id { get; set; }
				public Address Address { get; set; }
				public DateTimeOffset RemoveddAt { get; set; }
			}
		}
	}
}
