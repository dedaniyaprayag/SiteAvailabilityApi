using Dapper;
using Npgsql;
using SiteAvailabilityApi.Config;
using SiteAvailabilityApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteAvailabilityApi.Services
{
    public class SiteAvailabilityProvider : ISiteAvailabilityProvider
    {
        private readonly IPostgreSqlConfiguration _postgreConfiguration;

        public SiteAvailabilityProvider(IPostgreSqlConfiguration postgreConfiguration)
        {
            _postgreConfiguration = postgreConfiguration;
        }

        public async Task<IEnumerable<Site>> GetSiteHistoryByUser(string userid)
        {
            using var conn = new NpgsqlConnection(_postgreConfiguration.ConnectionString);
            conn.Open();
            string getAllSitesByUserId = @"SELECT * FROM patient WHERE userid=" + userid;
            using var command = new NpgsqlCommand(getAllSitesByUserId, conn);
            return await conn.QueryAsync<Site>(getAllSitesByUserId);
        }
    }

}
