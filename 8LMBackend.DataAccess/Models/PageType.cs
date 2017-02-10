using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PageType
    {
        public PageType()
        {
            Pages = new HashSet<Pages>();
            PageToolbox = new HashSet<PageToolbox>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pages> Pages { get; set; }
        public virtual ICollection<PageToolbox> PageToolbox { get; set; }
    }
}
