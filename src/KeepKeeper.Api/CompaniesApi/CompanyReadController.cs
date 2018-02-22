using System;
using KeepKeeper.Api.CompaniesApi.Projections;
using KeepKeeper.Framework;
using Microsoft.AspNetCore.Mvc;

namespace KeepKeeper.Api.CompaniesApi
{
	public class CompanyReadController : Controller
	{
		private readonly IRepository repository;

		public CompanyReadController(IRepository repository)
		{
			this.repository = repository;
		}

		[HttpGet]
		[Route("company/preview/{id}")]
		public CompanyShortDocument GetCompanyPreview(Guid id)
		{
			var company = repository.Load<CompanyShortDocument>(
				nameof(CompanyShort), id);

			return company;
		}
	}
}