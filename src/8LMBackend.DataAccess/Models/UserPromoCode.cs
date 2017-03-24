using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class UserPromoCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual Users User { get; set; }
    }
}
