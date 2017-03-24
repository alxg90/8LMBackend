using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class ControlType
    {
        public ControlType()
        {
            PageToolbox = new HashSet<PageToolbox>();
        }

        public int Id { get; set; }
        public int GroupId { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PageToolbox> PageToolbox { get; set; }
        public virtual ControlGroup Group { get; set; }
    }
}
