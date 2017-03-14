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
            UserroleCreatedByNavigation = new HashSet<Userrole>();
            UserroleUser = new HashSet<Userrole>();
            Usertoken = new HashSet<Usertoken>();
        }

        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClearPassword { get; set; }
        public string Email { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<Campaign> Campaign { get; set; }
        public virtual ICollection<Campaigncategory> Campaigncategory { get; set; }
        public virtual ICollection<Campaignshare> CampaignshareCreatedByNavigation { get; set; }
        public virtual ICollection<Campaignshare> CampaignshareUser { get; set; }
        public virtual ICollection<Pages> Pages { get; set; }
        public virtual ICollection<Rolefunction> Rolefunction { get; set; }
        public virtual ICollection<Securityrole> Securityrole { get; set; }
        public virtual ICollection<Usercompany> Usercompany { get; set; }
        public virtual ICollection<Usercontact> Usercontact { get; set; }
        public virtual ICollection<Userrole> UserroleCreatedByNavigation { get; set; }
        public virtual ICollection<Userrole> UserroleUser { get; set; }
        public virtual ICollection<Usertoken> Usertoken { get; set; }
        public virtual Userstatus Status { get; set; }
        public virtual Usertype Type { get; set; }
    }
}
