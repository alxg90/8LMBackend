using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PageToolbox
    {
        public int ControlTypeId { get; set; }
        public int PageTypeId { get; set; }

        public virtual ControlType ControlType { get; set; }
        public virtual PageType PageType { get; set; }
    }
}
