using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Campaign
    {
        public Campaign()
        {
            CampaignShare = new HashSet<CampaignShare>();
            CampaignTag = new HashSet<CampaignTag>();
            PageCampaign = new HashSet<PageCampaign>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }

        public virtual ICollection<CampaignShare> CampaignShare { get; set; }
        public virtual ICollection<CampaignTag> CampaignTag { get; set; }
        public virtual ICollection<PageCampaign> PageCampaign { get; set; }
        public virtual CampaignCategory Category { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
        public virtual CampaignStatus Status { get; set; }
    }
}
