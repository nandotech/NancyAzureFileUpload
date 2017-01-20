using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Owin;

namespace NancyAzureFileUpload
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) 
        {
            // CustomBootstrapper.cs
        }
        public void Configure(IApplicationBuilder app) 
        {
            app.UseOwin(x=>x.UseNancy());
        }
        
    }
}