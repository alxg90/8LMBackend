using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class UserToken
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Seed { get; set; }
        public int StatusId { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual EntityStatus Status { get; set; }
        public virtual Users User { get; set; }
    }
}
