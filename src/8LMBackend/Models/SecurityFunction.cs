using System;
using System.Collections.Generic;

namespace _8LMCore.Models
{
    public partial class SecurityFunction
    {
        public SecurityFunction()
        {
            RoleFunction = new HashSet<RoleFunction>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActual { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RoleFunction> RoleFunction { get; set; }
    }
}
