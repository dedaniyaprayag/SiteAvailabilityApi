using SiteAvailabilityApi.Models;
using System.Threading.Tasks;

namespace SiteAvailabilityApi.Services
{
    public interface IAvailabilityService
    {
        Task SendSiteToQueue(Site site);
    }
}
