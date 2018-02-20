using System;

namespace KeepKeeper.Api.Contarcts
{
	public static class CompanyCommands
	{
		public static class V1
		{
			public class Create
			{
				public Guid OwnerId { get; set; }
				
				public DateTimeOffset CreatedAt { get; set; }

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


			public class AddAddress
			{
				public Guid CompanyId { get; set; }

				public DateTimeOffset AddedAt { get; set; }

				public string AddressLine1 { get; set; }

				public string AddressLine2 { get; set; }

				public string AddressPostCode { get; set; }

				public string AddressCity { get; set; }

				public string AddressCountry { get; set; }

				public override string ToString() => $"Adding new address to company {CompanyId}";
			}

			public class ChangeAddress
			{

				public Guid CompanyId { get; set; }

				public DateTimeOffset ChangedAt { get; set; }

				public string AddressLine1 { get; set; }

				public string AddressLine2 { get; set; }

				public string AddressPostCode { get; set; }

				public string AddressCity { get; set; }

				public string AddressCountry { get; set; }

				public override string ToString() => $"Changing address of company {CompanyId}";
			}

			public class RemoveAddress
			{

				public Guid CompanyId { get; set; }

				public DateTimeOffset RemovedAt { get; set; }

				public override string ToString() => $"Removing address of company {CompanyId}";
			}
		}
	}
}
