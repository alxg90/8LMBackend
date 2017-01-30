using System;
using System.Collections.Generic;

namespace _8LMCore.Models
{
    public partial class RoleFunction
    {
        public int RoleId { get; set; }
        public int FunctionId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual SecurityFunction Function { get; set; }
        public virtual SecurityRole Role { get; set; }
    }
}
