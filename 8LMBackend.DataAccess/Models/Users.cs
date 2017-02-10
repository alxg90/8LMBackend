using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Users
    {
        public Users()
        {
            Campaign = new HashSet<Campaign>();
            CampaignCategory = new HashSet<CampaignCategory>();
            CampaignShareCreatedByNavigation = new HashSet<CampaignShare>();
            CampaignShareUser = new HashSet<CampaignShare>();
            Pages = new HashSet<Pages>();
            RoleFunction = new HashSet<RoleFunction>();
            SecurityRole = new HashSet<SecurityRole>();
            UserTokenCreatedByNavigation = new HashSet<UserToken>();
            UserTokenUser = new HashSet<UserToken>();
        }

        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Login { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }

        public virtual ICollection<Campaign> Campaign { get; set; }
        public virtual ICollection<CampaignCategory> CampaignCategory { get; set; }
        public virtual ICollection<CampaignShare> CampaignShareCreatedByNavigation { get; set; }
        public virtual ICollection<CampaignShare> CampaignShareUser { get; set; }
        public virtual ICollection<Pages> Pages { get; set; }
        public virtual ICollection<RoleFunction> RoleFunction { get; set; }
        public virtual ICollection<SecurityRole> SecurityRole { get; set; }
        public virtual ICollection<UserToken> UserTokenCreatedByNavigation { get; set; }
        public virtual ICollection<UserToken> UserTokenUser { get; set; }
        public virtual UserStatus Status { get; set; }
        public virtual UserType Type { get; set; }
    }
}
