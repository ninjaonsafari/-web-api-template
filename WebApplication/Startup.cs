using Microsoft.Extensions.DependencyInjection;

using Owin;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Routing;

using WebApplication.Extensions;
using WebApplication.Resolvers;
using WebApplication.Services;

namespace WebApplication
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var services = new ServiceCollection();
			ConfigureServices(services);

			// Set WebAPI Resolver and register
			HttpConfiguration config = new HttpConfiguration();
			config.DependencyResolver = new DefaultDependencyResolver(services.BuildServiceProvider());

			// WebApi Route
			config.Routes.MapHttpRoute(
					name: "MyDefaultApi",
					routeTemplate: "api/{controller}/{id}",
					defaults: new { id = RouteParameter.Optional }
		 );

			app.UseWebApi(config);
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersAsServices(typeof(Startup).Assembly.GetExportedTypes()
					.Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
					.Where(t => typeof(IController).IsAssignableFrom(t)
											|| t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));

			services.AddSingleton<IUserService, UserService>();
		}
	}
}