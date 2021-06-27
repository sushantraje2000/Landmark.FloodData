using Landmark.FloodData.App_Start;
using System.Web.Http;

namespace Landmark.FloodData
{
    public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();
			StructuremapWebApi.Start();
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/v1/{controller}/{region}",
				defaults: new { region = RouteParameter.Optional }
			);
		}
	}
}
