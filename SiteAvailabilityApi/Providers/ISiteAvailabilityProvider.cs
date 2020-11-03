using SiteAvailabilityApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteAvailabilityApi.Services
{
    public interface ISiteAvailabilityProvider
    {
        Task<IEnumerable<SiteDto>> GetSiteHistoryByUser(string userid);
    }

}
