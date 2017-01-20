using Microsoft.Extensions.Configuration;
using Nancy;

namespace NancyAzureFileUpload.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule(IConfiguration _config)
        {
            Get("/", args =>
            {
                return _config["Greeting"];
            });
        }
    }
}