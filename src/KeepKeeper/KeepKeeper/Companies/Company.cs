using KeepKeeper.Common;
using KeepKeeper.Framework;
using System;

namespace KeepKeeper.Companies
{
	public class Company : Aggregate
	{
		private Name name;

		private Email email;

		private VatNumber vatNumber;

		private Address address;

		//private Picture logo;

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

				case Events.V1.CompanyAddressAdded x:
					address = x.Address;
					break;

				case Events.V1.CompanyAddressChanged x:
					address = x.Address;
					break;

				case Events.V1.CompanyEmailChanged x:
					email = x.Email;
					break;
			}
		}

		public static Company Create(
			Name name,
			VatNumber vatNumber,
			TenantId tenantId,
			DateTimeOffset createdAt)
		{
			var company = new Company();
			company.Apply(new Events.V1.CompanyCreated
			{
				Id = Guid.NewGuid(),
				Name = name,
				VatNumber = vatNumber,
				TenantId = tenantId,
				CreatedAt = createdAt
			});
			return company;
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

		public void ChangeEmail(Email email, DateTimeOffset chamgedAt)
		{
			if (Version == -1)
				throw new Exceptions.ComapnyNotFoundException();

			Apply(new Events.V1.CompanyEmailChanged
			{
				Id = Id,
				Email = email,
				ChangedAt = chamgedAt
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

		public void AddAddress(Address address, DateTimeOffset addedAt)
		{
			if (Version == -1)
				throw new Exceptions.ComapnyNotFoundException();

			Apply(new Events.V1.CompanyAddressAdded
			{
				Id = Id,
				Address = address,
				AddedAt = addedAt
			});
		}

		public void ChangeAddress(Address newAddress, DateTimeOffset changedAt)
		{
			if (Version == -1)
				throw new Exceptions.ComapnyNotFoundException();

			Apply(new Events.V1.CompanyAddressChanged
			{
				Id = Id,
				Address = newAddress,
				ChangedAt = changedAt
			});
		}

		public void RemoveAddress(DateTimeOffset removedAt)
		{
			if (Version == -1)
				throw new Exceptions.ComapnyNotFoundException();

			Apply(new Events.V1.CompanyAddressChanged
			{
				Id = Id,
				Address = null,
				ChangedAt = removedAt
			});
		}
	}
}
