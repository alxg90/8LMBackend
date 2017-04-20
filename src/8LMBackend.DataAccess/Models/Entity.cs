using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Entity
    {
        public Entity()
        {
            EntityStatus = new HashSet<EntityStatus>();
            EntityType = new HashSet<EntityType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EntityStatus> EntityStatus { get; set; }
        public virtual ICollection<EntityType> EntityType { get; set; }
    }
}
