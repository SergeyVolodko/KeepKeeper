using System;
using Microsoft.AspNetCore.Mvc;

namespace KeepKeeper.Api
{
	[Route("/test")]
	public class TestApi : Controller
	{
		[HttpGet]
		public DateTime Get() => DateTime.Now;
	}
}
