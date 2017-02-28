using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Contacttype
    {
        public Contacttype()
        {
            Usercontact = new HashSet<Usercontact>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Usercontact> Usercontact { get; set; }
    }
}
