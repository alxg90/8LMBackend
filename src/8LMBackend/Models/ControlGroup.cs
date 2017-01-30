using System;
using System.Collections.Generic;

namespace _8LMCore.Models
{
    public partial class ControlGroup
    {
        public ControlGroup()
        {
            ControlType = new HashSet<ControlType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ControlType> ControlType { get; set; }
    }
}
