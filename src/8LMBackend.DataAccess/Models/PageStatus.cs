﻿using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PageStatus
    {
        public PageStatus()
        {
            Pages = new HashSet<Pages>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pages> Pages { get; set; }
    }
}
