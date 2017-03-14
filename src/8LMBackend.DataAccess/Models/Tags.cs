using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Tags
    {
        public Tags()
        {
            Campaigntag = new HashSet<Campaigntag>();
            Pagetag = new HashSet<Pagetag>();
        }

        public int Id { get; set; }
        public string Tag { get; set; }

        public virtual ICollection<Campaigntag> Campaigntag { get; set; }
        public virtual ICollection<Pagetag> Pagetag { get; set; }
    }
}
