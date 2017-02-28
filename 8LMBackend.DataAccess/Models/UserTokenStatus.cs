using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Usertokenstatus
    {
        public Usertokenstatus()
        {
            Usertoken = new HashSet<Usertoken>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Usertoken> Usertoken { get; set; }
    }
}
