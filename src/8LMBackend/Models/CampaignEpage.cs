﻿using System;
using System.Collections.Generic;

namespace _8LMCore.Models
{
    public partial class CampaignEpage
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int EpageId { get; set; }

        public virtual Campaign Campaign { get; set; }
    }
}
