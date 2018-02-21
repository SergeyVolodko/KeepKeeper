using System;
using Microsoft.AspNetCore.Mvc;

namespace KeepKeeper.Api
{
	public class TestApi : Controller
	{
		[HttpGet]
		[Route("test")]
		public DateTime Get() => DateTime.Now;
	}
}
