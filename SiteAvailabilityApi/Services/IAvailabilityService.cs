using SiteAvailabilityApi.Models;
using System.Threading.Tasks;

namespace SiteAvailabilityApi.Services
{
    public interface IAvailabilityService
    {
        void SendSiteToQueue(Site site);
    }
}
