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
				public Guid CreatedBy { get; set; }

				public override string ToString()
					=> $"Company {Id} created.";
			}

			public class CompanyRenamed
			{
				public Guid Id { get; set; }
				public string Name { get; set; }
				public DateTimeOffset RenamedAt { get; set; }
				public Guid RenamedBy { get; set; }

				public override string ToString()
					=> $"Comapny {Id} renamed to '{(Name?.Length > 25 ? $"{Name?.Substring(0, 22)}..." : Name)}'";
			}

			public class CompanyVatNumberChanged
			{
				public Guid Id { get; set; }
				public string VatNumber { get; set; }
				public DateTimeOffset RenamedAt { get; set; }
				public Guid RenamedBy { get; set; }

				public override string ToString()
					=> $"Comapny {Id} VatNumber changed to '{VatNumber}'";
			}
		}
	}
}
