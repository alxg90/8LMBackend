using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class RoleService
    {
        public int RoleId { get; set; }
        public int ServiceId { get; set; }
        
        public virtual Service Service { get; set; }
        public virtual SecurityRole Role { get; set; }
    }
}
