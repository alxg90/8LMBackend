using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Pagetoolbox
    {
        public int Id { get; set; }
        public int ControlTypeId { get; set; }
        public int PageTypeId { get; set; }

        public virtual Controltype ControlType { get; set; }
        public virtual Pagetype PageType { get; set; }
    }
}
