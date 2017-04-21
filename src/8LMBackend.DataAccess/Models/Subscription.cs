using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Subscription
    {
        public Subscription()
        {
            SubscriptionExtraService = new HashSet<SubscriptionExtraService>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int PackageRatePlanId { get; set; }
        public int RelayAuthorizeNetresponse { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<SubscriptionExtraService> SubscriptionExtraService { get; set; }
        public virtual PackageRatePlan PackageRatePlan { get; set; }
        public virtual EntityStatus Status { get; set; }
        public virtual Users User { get; set; }
    }
}
