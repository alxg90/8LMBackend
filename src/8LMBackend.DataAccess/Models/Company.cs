using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Company
    {
        public Company()
        {
            UserCompany = new HashSet<UserCompany>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserCompany> UserCompany { get; set; }
    }
}
