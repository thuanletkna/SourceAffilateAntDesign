using AffilateSource.Data.Services.Interface;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AffilateSource.Data.Services.Repository
{
    public class SequenceService : ISequenceService
    {
        private readonly IConfiguration _configuration;
        public SequenceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> GetKnowledgeBaseNewId()
        {
            using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }

            var result = await conn.ExecuteScalarAsync<int>(@"SELECT (NEXT VALUE FOR ProductAffilatesSequences)", null, null, 120, CommandType.Text);
            return result;
        }
        public async Task<int> GetCategoryNewId()
        {
            using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }

            var result = await conn.ExecuteScalarAsync<int>(@"SELECT (NEXT VALUE FOR ProductAffilatesSequences)", null, null, 120, CommandType.Text);
            return result;
        }
    }
}
