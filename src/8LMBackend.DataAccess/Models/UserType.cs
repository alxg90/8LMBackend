﻿using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Package = new HashSet<Package>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Package> Package { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
