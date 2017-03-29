using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class ServiceFunction
    {
        public int ServiceId { get; set; }
        public int SecurityFunctionId { get; set; }
        public int CreatedBy { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual SecurityFunction SecurityFunction { get; set; }
    }
}
