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
            Package = new HashSet<Package>();
            PackagePrice = new HashSet<PackagePrice>();
            Pages = new HashSet<Pages>();
            PromoProduct = new HashSet<PromoProduct>();
            RoleFunction = new HashSet<RoleFunction>();
            SecurityRole = new HashSet<SecurityRole>();
            UserCompany = new HashSet<UserCompany>();
            UserContact = new HashSet<UserContact>();
            UserPromoCodeCreatedByNavigation = new HashSet<UserPromoCode>();
            UserPromoCodeUser = new HashSet<UserPromoCode>();
            UserRoleCreatedByNavigation = new HashSet<UserRole>();
            UserRoleUser = new HashSet<UserRole>();
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
        public virtual ICollection<Package> Package { get; set; }
        public virtual ICollection<PackagePrice> PackagePrice { get; set; }
        public virtual ICollection<Pages> Pages { get; set; }
        public virtual ICollection<PromoProduct> PromoProduct { get; set; }
        public virtual ICollection<RoleFunction> RoleFunction { get; set; }
        public virtual ICollection<SecurityRole> SecurityRole { get; set; }
        public virtual ICollection<UserCompany> UserCompany { get; set; }
        public virtual ICollection<UserContact> UserContact { get; set; }
        public virtual ICollection<UserPromoCode> UserPromoCodeCreatedByNavigation { get; set; }
        public virtual ICollection<UserPromoCode> UserPromoCodeUser { get; set; }
        public virtual ICollection<UserRole> UserRoleCreatedByNavigation { get; set; }
        public virtual ICollection<UserRole> UserRoleUser { get; set; }
        public virtual ICollection<UserToken> UserTokenCreatedByNavigation { get; set; }
        public virtual ICollection<UserToken> UserTokenUser { get; set; }
        public virtual UserStatus Status { get; set; }
        public virtual UserType Type { get; set; }
    }
}
