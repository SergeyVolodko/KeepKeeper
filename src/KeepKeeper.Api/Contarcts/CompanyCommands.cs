using System;

namespace KeepKeeper.Api.Contarcts
{
	public static class CompanyCommands
	{
		public static class V1
		{
			public class Create
			{
				public Guid TenantId { get; set; }

				public string CompanyName { get; set; }

				public string VatNumber { get; set; }

				public override string ToString() => $"Creating new company";
			}

			public class Rename
			{
				public Guid CompanyId { get; set; }

				public DateTimeOffset RenamedAt { get; set; }

				public string NewName { get; set; }

				public override string ToString() => $"Renaming company {CompanyId}";
			}

			public class ChangeVatNumber
			{
				public Guid CompanyId { get; set; }

				public DateTimeOffset ChangedAt { get; set; }

				public string NewVatNumber { get; set; }

				public override string ToString() => $"Changing company VAT number {CompanyId}";
			}
		}
	}
}
