using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class EntityStatus
    {
        public EntityStatus()
        {
            Campaign = new HashSet<Campaign>();
            Invoice = new HashSet<Invoice>();
            Package = new HashSet<Package>();
            Pages = new HashSet<Pages>();
            Subscription = new HashSet<Subscription>();
            Users = new HashSet<Users>();
            UserToken = new HashSet<UserToken>();
        }

        public int Id { get; set; }
        public int EntityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Campaign> Campaign { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<Package> Package { get; set; }
        public virtual ICollection<Pages> Pages { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
        public virtual ICollection<Users> Users { get; set; }
        public virtual ICollection<UserToken> UserToken { get; set; }
        public virtual Entity Entity { get; set; }
    }
}
