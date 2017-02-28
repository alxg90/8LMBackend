using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Users
    {
        public Users()
        {
            Campaign = new HashSet<Campaign>();
            Campaigncategory = new HashSet<Campaigncategory>();
            CampaignshareCreatedByNavigation = new HashSet<Campaignshare>();
            CampaignshareUser = new HashSet<Campaignshare>();
            Pages = new HashSet<Pages>();
            Rolefunction = new HashSet<Rolefunction>();
            Securityrole = new HashSet<Securityrole>();
            Usercompany = new HashSet<Usercompany>();
            Usercontact = new HashSet<Usercontact>();
            Userpromocode = new HashSet<Userpromocode>();
            UserroleCreatedByNavigation = new HashSet<Userrole>();
            UserroleUser = new HashSet<Userrole>();
            UsertokenCreatedByNavigation = new HashSet<Usertoken>();
            UsertokenUser = new HashSet<Usertoken>();
        }

        public int Id { get; set; }
        public int? CId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? IndustryId { get; set; }
        public int? LinenameId { get; set; }
        public string Login { get; set; }
        public int? SalesId { get; set; }
        public int? SourceId { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }

        public virtual ICollection<Campaign> Campaign { get; set; }
        public virtual ICollection<Campaigncategory> Campaigncategory { get; set; }
        public virtual ICollection<Campaignshare> CampaignshareCreatedByNavigation { get; set; }
        public virtual ICollection<Campaignshare> CampaignshareUser { get; set; }
        public virtual ICollection<Pages> Pages { get; set; }
        public virtual ICollection<Rolefunction> Rolefunction { get; set; }
        public virtual ICollection<Securityrole> Securityrole { get; set; }
        public virtual ICollection<Usercompany> Usercompany { get; set; }
        public virtual ICollection<Usercontact> Usercontact { get; set; }
        public virtual ICollection<Userpromocode> Userpromocode { get; set; }
        public virtual ICollection<Userrole> UserroleCreatedByNavigation { get; set; }
        public virtual ICollection<Userrole> UserroleUser { get; set; }
        public virtual ICollection<Usertoken> UsertokenCreatedByNavigation { get; set; }
        public virtual ICollection<Usertoken> UsertokenUser { get; set; }
        public virtual Userstatus Status { get; set; }
        public virtual Usertype Type { get; set; }
    }
}
