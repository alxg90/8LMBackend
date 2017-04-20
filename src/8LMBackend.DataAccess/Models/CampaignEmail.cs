using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class CampaignEmail
    {
        public int CampaignId { get; set; }
        public int EmailId { get; set; }

        public virtual Campaign Campaign { get; set; }
    }
}
