
using System.Net.Http.Formatting;
using System.Web.Http;

namespace KeeperApiClient
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.EnsureInitialized();
        }
    }
}
