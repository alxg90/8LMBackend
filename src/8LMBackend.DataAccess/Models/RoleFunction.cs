using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Rolefunction
    {
        public int RoleId { get; set; }
        public int FunctionId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual Securityfunction Function { get; set; }
        public virtual Securityrole Role { get; set; }
    }
}
