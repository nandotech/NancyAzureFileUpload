using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.TinyIoc;

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

                Configuration = builder.Build();
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer ctr)
        {
            ctr.Register<IConfiguration>(Configuration);
        }

    }
}