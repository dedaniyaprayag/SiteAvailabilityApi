using SiteAvailabilityApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteAvailabilityApi.Services
{
    public interface IDbProvider
    {
        Task<IEnumerable<SiteDto>> GetSiteHistoryByUser(string userid);
    }

}
