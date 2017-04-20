using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class CampaignEpage
    {
        public int CampaignId { get; set; }
        public int EpageId { get; set; }

        public virtual Campaign Campaign { get; set; }
    }
}
