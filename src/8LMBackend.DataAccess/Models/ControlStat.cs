﻿using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class ControlStat
    {
        public ControlStat()
        {
            PageStatistic = new HashSet<PageStatistic>();
        }

        public string Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public int PageId { get; set; }

        public virtual ICollection<PageStatistic> PageStatistic { get; set; }
        public virtual Pages Page { get; set; }
    }
}
