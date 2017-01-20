using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Nancy;
using NancyAzureFileUpload.Data;
using NancyAzureFileUpload.Services;

namespace NancyAzureFileUpload.Modules
{
    public class UploadModule : NancyModule
    {
        public UploadModule(IDispatchFileService _fileService,
                            IConfiguration _config)
        {
            Post("/upload", async (args, ct)  =>
            {
                var postedFile = Request.Files.FirstOrDefault();
                var queryInfo = Request.Form;                            
                DispatchFile fileInfo;
                
                if(postedFile != null) 
                {
                    //Check file type
                    var url = $"https://{_config["StorageAccount:User"]}.blob.core.windows.net/{_config["StorageAccount:containerName"]}/{postedFile.Name}";
                    var secondary = $"https://{_config["StorageAccount:User"]}-secondary.blob.core.windows.net/{_config["StorageAccount:containerName"]}/{postedFile.Name}";

                     //Upload file to Azure Storage
                    var creds = new StorageCredentials(_config["StorageAccount:User"], _config["StorageAccount:Key"]);
                    var blob = new CloudBlockBlob(new Uri(url), creds);
                    
                    await blob.UploadFromStreamAsync(postedFile.Value);
                    
                    // Create object to save to table             
                    fileInfo = new DispatchFile
                    {
                        DispatchId = Convert.ToInt32(queryInfo?.DispatchId?.Value ?? 0),
                        Filename = postedFile.Name,
                        Filetype = postedFile.ContentType,
                        PrimaryUrl = url,
                        SecondaryUrl = secondary,
                        ItemType = queryInfo?.ItemType?.Value
                    };
                    // Call our Dapper IDispatchFileService.cs
                    await _fileService.Add(fileInfo);

                }
                else 
                {
                    return "No files uploaded.";
                }
                

                return fileInfo;
            });

            Post("/upload/{dispatchId}/{itemType}", async (args, ct) =>
            {
                var postedFile = Request.Files.FirstOrDefault();
                var queryInfo = Request.Form;                            
                DispatchFile fileInfo;
                
                if(postedFile != null) 
                {
                    //Check file type
                    var url = $"https://{_config["StorageAccount:User"]}.blob.core.windows.net/{_config["StorageAccount:containerName"]}/{postedFile.Name}";
                    var secondary = $"https://{_config["StorageAccount:User"]}-secondary.blob.core.windows.net/{_config["StorageAccount:containerName"]}/{postedFile.Name}";

                    //Upload file to Azure Storage
                    var creds = new StorageCredentials(_config["StorageAccount:User"], _config["StorageAccount:Key"]);
                    var blob = new CloudBlockBlob(new Uri(url), creds);

                    await blob.UploadFromStreamAsync(postedFile.Value);  

                    // Create object to save to table               
                    fileInfo = new DispatchFile
                    {
                        Filename = postedFile.Name,
                        Filetype = postedFile.ContentType,
                        PrimaryUrl = url,
                        SecondaryUrl = secondary,
                        ItemType = args?.ItemType?.Value
                    };

                    // Call our Dapper IDispatchFileService.cs
                    await _fileService.Add(fileInfo);

                }
                else 
                {
                    return "No files uploaded.";
                }

                return fileInfo;
            });

        }
    }
}