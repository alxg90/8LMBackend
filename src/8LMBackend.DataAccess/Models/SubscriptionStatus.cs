using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class SubscriptionStatus
    {
        public SubscriptionStatus()
        {
            Subscription = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Subscription> Subscription { get; set; }
    }
}
