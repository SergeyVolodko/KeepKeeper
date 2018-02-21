using KeepKeeper.Api.CompaniesApi;
using KeepKeeper.Companies;
using KeepKeeper.Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Threading.Tasks;

namespace KeepKeeper.Api
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) =>
			ConfigureServicesAsync(services).GetAwaiter().GetResult();

		private async Task ConfigureServicesAsync(IServiceCollection services)
		{
			var esConnection = await Defaults.GetConnection();
			var typeMapper = ConfigureTypeMapper();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Event Log API", Version = "v1" });
			});
			
			services.AddSingleton<IAggregateStore>(new GesAggregateStore(
				(type, id) => $"{type.Name}-{id}",
				esConnection,
				new JsonNetSerializer(),
				typeMapper
			));
			services.AddTransient<CompanyService, CompanyService>();

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseMvcWithDefaultRoute();

			app.UseSwagger()
				.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Log API V1"); });
		}

		private static TypeMapper ConfigureTypeMapper()
		{
			var mapper = new TypeMapper();
			mapper.Map<Events.V1.CompanyCreated>("CompanyCreated");
			mapper.Map<Events.V1.CompanyRenamed>("CompanyRenamed");
			mapper.Map<Events.V1.CompanyVatNumberChanged>("CompanyVatNumberChanged");
			mapper.Map<Events.V1.CompanyAddressAdded>("CompanyAddressAdded");
			mapper.Map<Events.V1.CompanyAddressChanged>("CompanyAddressChanged");

			return mapper;
		}
	}
}
