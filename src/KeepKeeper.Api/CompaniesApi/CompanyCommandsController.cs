using KeepKeeper.Api.Contarcts;
using Microsoft.AspNetCore.Mvc;

namespace KeepKeeper.Api.CompaniesApi
{

	public class CompanyCommandsController : Controller
	{
		private readonly CompanyService service;

		public CompanyCommandsController(CompanyService service)
		{
			this.service = service;
		}

		[HttpPost]
		[Route("/company")]
		public void CreateCompany(
			CompanyCommands.V1.Create createCommand)
		{
			service.Handle(createCommand);
		}

		[HttpPost]
		[Route("/company/rename")]
		public async void RenameCompany(
			CompanyCommands.V1.Rename renameCommand)
		{
			await service.Handle(renameCommand);
		}

		[HttpPost]
		[Route("/company/change_vat")]
		public async void ChangeCompanyVatNumber(
			CompanyCommands.V1.ChangeVatNumber cahngeVatCommand)
		{
			await service.Handle(cahngeVatCommand);
		}
	}
}