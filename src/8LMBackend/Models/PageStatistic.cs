using System;
using System.Collections.Generic;

namespace _8LMCore.Models
{
    public partial class PageStatistic
    {
        public int Id { get; set; }
        public string ControlId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsLoad { get; set; }
        public int PageId { get; set; }

        public virtual Pages Page { get; set; }
    }
}
