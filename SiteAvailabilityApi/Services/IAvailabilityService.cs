using SiteAvailabilityApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteAvailabilityApi.Services
{
    public interface ISiteAvailablityService
    {
        void SendSiteToQueue(SiteDto site);
        Task<IEnumerable<SiteDto>> GetSiteHistoryByUser(string userid);
    }
}
