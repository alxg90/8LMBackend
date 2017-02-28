using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Pages
    {
        public Pages()
        {
            Pagecampaign = new HashSet<Pagecampaign>();
            Pagestatistic = new HashSet<Pagestatistic>();
            Pagetag = new HashSet<Pagetag>();
        }

        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Html { get; set; }
        public string Json { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }

        public virtual ICollection<Pagecampaign> Pagecampaign { get; set; }
        public virtual ICollection<Pagestatistic> Pagestatistic { get; set; }
        public virtual ICollection<Pagetag> Pagetag { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
        public virtual Pagestatus Status { get; set; }
        public virtual Pagetype Type { get; set; }
    }
}
