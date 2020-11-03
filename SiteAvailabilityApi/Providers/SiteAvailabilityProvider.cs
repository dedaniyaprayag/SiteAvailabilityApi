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

        public async Task<IEnumerable<SiteDto>> GetSiteHistoryByUser(string userid)
        {
            using var conn = new NpgsqlConnection(_postgreConfiguration.ConnectionString);
            conn.Open();
            string getAllSitesByUserId = @"SELECT * FROM sitehistory WHERE userid= '" + userid + "'";

            //string sql = @"INSERT INTO sitehistory (userid,site,status)
            //                     VALUES (@UserId,@Url,@Status)";

            // using var command = new NpgsqlCommand(sql, conn);
            return await conn.QueryAsync<SiteDto>(getAllSitesByUserId);
            //var site = new Site()
            //{
            //    Url = "jhgjhgh",
            //    UserId = "123",
            //    Status = 1
            //};
            //return await conn.ExecuteAsync(sql, site);
        }
    }

}
