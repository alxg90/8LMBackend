using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class CampaignShare
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
        public virtual Users User { get; set; }
    }
}
