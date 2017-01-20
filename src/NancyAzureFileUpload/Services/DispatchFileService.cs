using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NancyAzureFileUpload.Data;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Threading.Tasks;

namespace NancyAzureFileUpload.Services
{
    public class DispatchFileService : IDispatchFileService
    {
        private string dbConn;

        public DispatchFileService(IConfiguration _dbConnString)
        {
            dbConn = _dbConnString.GetConnectionString("DefaultConnection");
        }
        internal IDbConnection Connection 
        {
            get 
            {
                return new SqlConnection(dbConn);
            }
        }
        public async Task Add(DispatchFile postedFile)
        {
            string sQuery = @"INSERT INTO dcdb.Files
            (DispatchId, PrimaryUrl, SecondaryUrl, ItemType, Timestamp,
            Filename, Filetype)" +
            "VALUES(@DispatchId, @PrimaryUrl, @SecondaryUrl, @ItemType, getdate(), @Filename, @Filetype)";
            using (var conn = Connection)
            {
                conn.Open();
                await conn.ExecuteAsync(sQuery, postedFile);
            }
        }
    }
}