using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Pagecampaign
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int PageId { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Pages Page { get; set; }
    }
}
