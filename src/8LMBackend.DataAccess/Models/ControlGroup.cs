using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Controlgroup
    {
        public Controlgroup()
        {
            Controltype = new HashSet<Controltype>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Controltype> Controltype { get; set; }
    }
}
