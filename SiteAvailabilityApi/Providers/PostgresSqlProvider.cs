using Dapper;
using Npgsql;
using SiteAvailabilityApi.Config;
using SiteAvailabilityApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteAvailabilityApi.Services
{
    public class PostgresSqlProvider : IDbProvider
    {
        private readonly IPostgreSqlConfiguration _postgreConfiguration;

        public PostgresSqlProvider(IPostgreSqlConfiguration postgreConfiguration)
        {
            _postgreConfiguration = postgreConfiguration;
        }

        public async Task<IEnumerable<SiteDto>> GetSiteHistoryByUser(string userid)
        {
            using var conn = new NpgsqlConnection(_postgreConfiguration.ConnectionString);
            conn.Open();
            string getAllSitesByUserId = @"SELECT * FROM sitehistoricaldata WHERE userid= '" + userid + "'";
            return await conn.QueryAsync<SiteDto>(getAllSitesByUserId);
        }
    }

}
