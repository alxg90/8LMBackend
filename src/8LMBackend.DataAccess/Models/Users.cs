using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class distributors
    {
        public distributors()
        {
        }

        public int id { get; set; }
        public string phone { get; set; }
        public string company { get; set; }
        public string mailing_state { get; set; }
        public string mailing_address { get; set; }
        public string mailing_address2 { get; set; }
        public string mailing_city { get; set; }
        public string mailing_zip { get; set; }
        public string mailing_country { get; set; }
    }

    public partial class dist_epage_clicks
    {
        public dist_epage_clicks()
        {
        }

        public int id { get; set; }
        public int dist_id { get; set; }
        public DateTime created_at { get; set; }
    }

    public partial class dist_epage_views
    {
        public dist_epage_views()
        {
        }

        public int id { get; set; }
        public int dist_id { get; set; }
        public DateTime created_at { get; set; }
    }

    public partial class Users
    {
        public Users()
        {
            AuthorizeNetcustomerProfile = new HashSet<AuthorizeNetcustomerProfile>();
            Campaign = new HashSet<Campaign>();
            CampaignCategory = new HashSet<CampaignCategory>();
            CampaignShareCreatedByNavigation = new HashSet<CampaignShare>();
            CampaignShareUser = new HashSet<CampaignShare>();
            Invoice = new HashSet<Invoice>();
            Package = new HashSet<Package>();
            Pages = new HashSet<Pages>();
            RoleFunction = new HashSet<RoleFunction>();
            SecurityRole = new HashSet<SecurityRole>();
            Subscription = new HashSet<Subscription>();
            UserRoleCreatedByNavigation = new HashSet<UserRole>();
            UserRoleUser = new HashSet<UserRole>();
            UserTokenCreatedByNavigation = new HashSet<UserToken>();
            UserTokenUser = new HashSet<UserToken>();
        }

        public int Id { get; set; }
        public string ClearPassword { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Icon { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }

        public virtual ICollection<AuthorizeNetcustomerProfile> AuthorizeNetcustomerProfile { get; set; }
        public virtual ICollection<Campaign> Campaign { get; set; }
        public virtual ICollection<CampaignCategory> CampaignCategory { get; set; }
        public virtual ICollection<CampaignShare> CampaignShareCreatedByNavigation { get; set; }
        public virtual ICollection<CampaignShare> CampaignShareUser { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<Package> Package { get; set; }
        public virtual ICollection<Pages> Pages { get; set; }
        public virtual ICollection<RoleFunction> RoleFunction { get; set; }
        public virtual ICollection<SecurityRole> SecurityRole { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
        public virtual ICollection<UserRole> UserRoleCreatedByNavigation { get; set; }
        public virtual ICollection<UserRole> UserRoleUser { get; set; }
        public virtual ICollection<UserToken> UserTokenCreatedByNavigation { get; set; }
        public virtual ICollection<UserToken> UserTokenUser { get; set; }
        public virtual EntityStatus Status { get; set; }
        public virtual EntityType Type { get; set; }
    }
}
