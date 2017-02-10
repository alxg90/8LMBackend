using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class CampaignCategory
    {
        public CampaignCategory()
        {
            Campaign = new HashSet<Campaign>();
        }

        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public bool IsActual { get; set; }
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }

        public virtual ICollection<Campaign> Campaign { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
    }
}
