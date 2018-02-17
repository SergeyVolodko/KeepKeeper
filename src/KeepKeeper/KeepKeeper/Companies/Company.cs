using KeepKeeper.Common;
using KeepKeeper.Framework;
using System;

namespace KeepKeeper.Companies
{
    public class Company : Aggregate
	{
		private Name name;

		private VatNumber vatNumber;

		// add address

		protected override void When(object e)
		{
			switch (e)
			{
				case Events.V1.CompanyCreated x:
					Id = x.Id;
					name = x.Name;
					vatNumber = x.VatNumber;
					break;

				case Events.V1.CompanyRenamed x:
					name = x.Name;
					break;

				case Events.V1.CompanyVatNumberChanged x:
					vatNumber = x.VatNumber;
					break;
			}
		}

		public static Company Create(CompanyId id, TenantId owner, TenantId createdBy, DateTimeOffset createdAt)
		{
			var comapny = new Company();
			comapny.Apply(new Events.V1.CompanyCreated
			{
				Id = id,
				Owner = owner,
				CreatedBy = createdBy,
				CreatedAt = createdAt
			});
			return comapny;
		}

		public void Rename(Name name, DateTimeOffset renamedAt, TenantId renamedBy)
		{
			if (Version == -1)
				throw new Exceptions.ComapnyNotFoundException();

			Apply(new Events.V1.CompanyRenamed
			{
				Id = Id,
				Name = name,
				RenamedAt = renamedAt,
				RenamedBy = renamedBy
			});
		}

		public void ChangeVatNumber(VatNumber vatNumber, DateTimeOffset renamedAt, TenantId renamedBy)
		{
			if (Version == -1)
				throw new Exceptions.ComapnyNotFoundException();

			Apply(new Events.V1.CompanyVatNumberChanged
			{
				Id = Id,
				VatNumber = vatNumber,
				RenamedAt = renamedAt,
				RenamedBy = renamedBy
			});
		}
	}
}
