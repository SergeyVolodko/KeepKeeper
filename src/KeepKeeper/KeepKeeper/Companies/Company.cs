using KeepKeeper.Common;
using KeepKeeper.Framework;
using System;

namespace KeepKeeper.Companies
{
    public class Company : Aggregate
	{
		private Name name;

		private VatNumber vatNumber;

		private Address address;

		private Picture logo;

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

		public static Company Create(
			Name name, 
			VatNumber vatNumber, 
			TenantId owner,
			DateTimeOffset createdAt)
		{
			var comapny = new Company();
			comapny.Apply(new Events.V1.CompanyCreated
			{
				Id = Guid.NewGuid(),
				Owner = owner,
				CreatedAt = createdAt
			});
			return comapny;
		}

		public void Rename(Name newName, DateTimeOffset renamedAt)
		{
			if (Version == -1)
				throw new Exceptions.ComapnyNotFoundException();

			Apply(new Events.V1.CompanyRenamed
			{
				Id = Id,
				Name = newName,
				RenamedAt = renamedAt
			});
		}

		public void ChangeVatNumber(VatNumber vatNumber, DateTimeOffset chamgedAt)
		{
			if (Version == -1)
				throw new Exceptions.ComapnyNotFoundException();

			Apply(new Events.V1.CompanyVatNumberChanged
			{
				Id = Id,
				VatNumber = vatNumber,
				ChangedAt = chamgedAt
			});
		}
	}
}
