using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class CampaignProduct
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int ProductId { get; set; }

        public virtual Campaign Campaign { get; set; }
    }
}
