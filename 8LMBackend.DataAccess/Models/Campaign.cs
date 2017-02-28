using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Campaign
    {
        public Campaign()
        {
            Campaignshare = new HashSet<Campaignshare>();
            Campaigntag = new HashSet<Campaigntag>();
            Pagecampaign = new HashSet<Pagecampaign>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }

        public virtual ICollection<Campaignshare> Campaignshare { get; set; }
        public virtual ICollection<Campaigntag> Campaigntag { get; set; }
        public virtual ICollection<Pagecampaign> Pagecampaign { get; set; }
        public virtual Campaigncategory Category { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
        public virtual Campaignstatus Status { get; set; }
    }
}
