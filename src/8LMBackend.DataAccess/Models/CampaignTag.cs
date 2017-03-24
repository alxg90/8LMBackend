﻿using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class CampaignTag
    {
        public int CampaignId { get; set; }
        public int TagId { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Tags Tag { get; set; }
    }
}
