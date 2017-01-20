using Nancy;
using System.IO;

namespace NancyAzureFileUpload.Helpers
{
    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath() 
        {
            return Directory.GetCurrentDirectory();
        }
    }
}