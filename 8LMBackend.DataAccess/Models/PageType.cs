using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Pagetype
    {
        public Pagetype()
        {
            Pages = new HashSet<Pages>();
            Pagetoolbox = new HashSet<Pagetoolbox>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pages> Pages { get; set; }
        public virtual ICollection<Pagetoolbox> Pagetoolbox { get; set; }
    }
}
