using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual SecurityRole Role { get; set; }
        public virtual Users User { get; set; }
    }
}
