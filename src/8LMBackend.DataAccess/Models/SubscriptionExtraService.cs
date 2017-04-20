using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class SubscriptionExtraService
    {
        public int SubscriptionId { get; set; }
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}
