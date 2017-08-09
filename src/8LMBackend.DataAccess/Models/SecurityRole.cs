using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class SecurityRole
    {
        public SecurityRole()
        {
            RoleFunction = new HashSet<RoleFunction>();
            RoleService = new HashSet<RoleService>();
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public bool IsActual { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RoleFunction> RoleFunction { get; set; }
        public virtual ICollection<RoleService> RoleService { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
    }
}
