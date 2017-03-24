using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class ContactType
    {
        public ContactType()
        {
            UserContact = new HashSet<UserContact>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserContact> UserContact { get; set; }
    }
}
