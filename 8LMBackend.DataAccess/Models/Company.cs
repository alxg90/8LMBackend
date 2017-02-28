using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Company
    {
        public Company()
        {
            Usercompany = new HashSet<Usercompany>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Usercompany> Usercompany { get; set; }
    }
}
