using System.Collections.Generic;
using System.Threading.Tasks;
using NancyAzureFileUpload.Data;

namespace NancyAzureFileUpload.Services
{
    public interface IDispatchFileService
    {
         Task Add(DispatchFile postedFile);

    }
}