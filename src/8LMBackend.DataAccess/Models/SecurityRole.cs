using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Securityrole
    {
        public Securityrole()
        {
            Rolefunction = new HashSet<Rolefunction>();
            Userrole = new HashSet<Userrole>();
        }

        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public bool IsActual { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Rolefunction> Rolefunction { get; set; }
        public virtual ICollection<Userrole> Userrole { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
    }
}
