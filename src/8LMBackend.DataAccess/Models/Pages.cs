﻿using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Pages
    {
        public Pages()
        {
            ControlStat = new HashSet<ControlStat>();
            PageCampaign = new HashSet<PageCampaign>();
            PageStatistic = new HashSet<PageStatistic>();
            PageTag = new HashSet<PageTag>();
        }

        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Html { get; set; }
        public string Json { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public bool IsPublic { get; set; }

        public virtual ICollection<ControlStat> ControlStat { get; set; }
        public virtual ICollection<PageCampaign> PageCampaign { get; set; }
        public virtual ICollection<PageStatistic> PageStatistic { get; set; }
        public virtual ICollection<PageTag> PageTag { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
        public virtual EntityStatus Status { get; set; }
        public virtual EntityType Type { get; set; }
    }
}
