using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.TinyIoc;
using NancyAzureFileUpload.Services;

namespace NancyAzureFileUpload.Helpers
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        public IConfigurationRoot Configuration;
        public CustomBootstrapper()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(RootPathProvider.GetRootPath())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            var tracingConfig = new TraceConfiguration(true, true);

                Configuration = builder.Build();
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer ctr)
        {
            ctr.Register<IConfiguration>(Configuration);
            ctr.Register<IDispatchFileService, DispatchFileService>();
        }

    }
}