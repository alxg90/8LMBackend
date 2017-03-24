using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
