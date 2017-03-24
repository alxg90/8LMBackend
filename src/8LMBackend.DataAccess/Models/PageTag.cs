using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PageTag
    {
        public int PageId { get; set; }
        public int TagId { get; set; }

        public virtual Pages Page { get; set; }
        public virtual Tags Tag { get; set; }
    }
}
