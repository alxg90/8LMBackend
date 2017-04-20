using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class EntityType
    {
        public EntityType()
        {
            Package = new HashSet<Package>();
            Pages = new HashSet<Pages>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public int EntityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Package> Package { get; set; }
        public virtual ICollection<Pages> Pages { get; set; }
        public virtual ICollection<Users> Users { get; set; }
        public virtual Entity Entity { get; set; }
    }
}
