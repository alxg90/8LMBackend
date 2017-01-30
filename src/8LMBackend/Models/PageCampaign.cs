using System;
using System.Collections.Generic;

namespace _8LMCore.Models
{
    public partial class PageCampaign
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int PageId { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Pages Page { get; set; }
    }
}
