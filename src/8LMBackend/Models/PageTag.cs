using System;
using System.Collections.Generic;

namespace _8LMCore.Models
{
    public partial class PageTag
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public int TagId { get; set; }

        public virtual Pages Page { get; set; }
        public virtual Tags Tag { get; set; }
    }
}
