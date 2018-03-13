using System;
using System.Collections.Generic;
using KeepKeeper.Api.Projections;
using KeepKeeper.Framework;
using Microsoft.AspNetCore.Mvc;

namespace KeepKeeper.Api.CompaniesApi
{
	public class CompanyReadController : Controller
	{
		private readonly IReadRepository repository;

		public CompanyReadController(IReadRepository repository)
		{
			this.repository = repository;
		}

		[HttpGet]
		[Route("company/preview")]
		public IList<CompanyShortDocument> GetCompaniesPreview()
		{
			var companies = repository.LoadMany<CompanyShortDocument>();

			return companies;
		}

		[HttpGet]
		[Route("company/{id}")]
		public CompanyDetailedDocument GetCompanyPreview(Guid id)
		{
			var company = repository.Load<CompanyDetailedDocument>(
				nameof(CompanyDetailed), id);

			return company;
		}
	}
}