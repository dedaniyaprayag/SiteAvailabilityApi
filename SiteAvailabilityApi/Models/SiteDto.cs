using System;

namespace SiteAvailabilityApi.Models
{
    public class SiteDto
    {
        public string UserId { get; set; }
        public string Site { get; set; }
        public bool Status { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

    }
}