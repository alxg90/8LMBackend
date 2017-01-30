using System;
using System.Collections.Generic;

namespace _8LMCore.Models
{
    public partial class CampaignEmail
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int EmailId { get; set; }

        public virtual Campaign Campaign { get; set; }
    }
}
