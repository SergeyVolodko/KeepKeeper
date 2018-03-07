using KeepKeeper.Api.Contarcts;
using KeepKeeper.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KeepKeeper.Api.CompaniesApi
{
	[Route("/company")]
	public class CompanyCommandsController : Controller
	{
		private readonly CompanyService service;

		public CompanyCommandsController(CompanyService service)
		{
			this.service = service;
		}

		[HttpPost]
		[Route("")]
		public void CreateCompany([FromBody]
			CompanyCommands.V1.Create createCommand)
		{
			service.Handle(createCommand);
		}

		[HttpPost]
		[Route("/rename")]
		public async void RenameCompany([FromBody]
			CompanyCommands.V1.Rename renameCommand)
		{
			await service.Handle(renameCommand);
		}

		[HttpPost]
		[Route("/change_email")]
		public Task<IActionResult> ChangeCompanyVatNumber([FromBody]
			CompanyCommands.V1.ChangeEmail changeEmail)
			=> HandleOrThrow(changeEmail, c => service.Handle(c));

		[HttpPost]
		[Route("/change_vat")]
		public Task<IActionResult> ChangeCompanyVatNumber([FromBody]
			CompanyCommands.V1.ChangeVatNumber changeVatCommand)
			=> HandleOrThrow(changeVatCommand, c => service.Handle(c));

		[HttpPost]
		[Route("/add_address")]
		public Task<IActionResult> AddCompanyAddress([FromBody]
			CompanyCommands.V1.AddAddress addAddressCommand)
			=> HandleOrThrow(addAddressCommand, c => service.Handle(c));

		[HttpPost]
		[Route("/change_address")]
		public Task<IActionResult> ChangeCompanyAddress([FromBody]
			CompanyCommands.V1.ChangeAddress changeAddressCommand)
			=> HandleOrThrow(changeAddressCommand, c => service.Handle(c));

		[HttpPost]
		[Route("/remove_address")]
		public Task<IActionResult> RemoveCompanyAddress([FromBody]
			CompanyCommands.V1.RemoveAddress removeAddressCommand)
			=> HandleOrThrow(removeAddressCommand, c => service.Handle(c));

		private async Task<IActionResult> HandleOrThrow<T>(T request, Func<T, Task> handler)
		{
			try
			{
				await handler(request);
				return Ok();
			}
			catch (Exceptions.ComapnyNotFoundException)
			{
				return NotFound();
			}
		}
	}
}