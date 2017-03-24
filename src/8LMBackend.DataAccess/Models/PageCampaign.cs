using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PageCampaign
    {
        public int CampaignId { get; set; }
        public int PageId { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Pages Page { get; set; }
    }
}
