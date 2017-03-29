using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Subscription
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int PackageId { get; set; }
        public int RelayAuthorizeNetresponse { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }

        public virtual Package Package { get; set; }
        public virtual SubscriptionStatus Status { get; set; }
        public virtual Users User { get; set; }
    }
}
