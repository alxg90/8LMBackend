using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Controltype
    {
        public Controltype()
        {
            Pagetoolbox = new HashSet<Pagetoolbox>();
        }

        public int Id { get; set; }
        public int GroupId { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pagetoolbox> Pagetoolbox { get; set; }
        public virtual Controlgroup Group { get; set; }
    }
}
