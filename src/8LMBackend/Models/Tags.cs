using System;
using System.Collections.Generic;

namespace _8LMCore.Models
{
    public partial class Tags
    {
        public Tags()
        {
            CampaignTag = new HashSet<CampaignTag>();
            PageTag = new HashSet<PageTag>();
        }

        public int Id { get; set; }
        public string Tag { get; set; }

        public virtual ICollection<CampaignTag> CampaignTag { get; set; }
        public virtual ICollection<PageTag> PageTag { get; set; }
    }
}
