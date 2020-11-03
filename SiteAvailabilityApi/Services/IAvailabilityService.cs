﻿using SiteAvailabilityApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteAvailabilityApi.Services
{
    public interface ISiteAvailablityService
    {
        void SendSiteToQueue(Site site);
        Task<IEnumerable<Site>> GetSiteHistoryByUser(string userid);
    }
}
