using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class AuthorizeNetcustomerProfile
    {
        public AuthorizeNetcustomerProfile()
        {
            AuthorizeNettransaction = new HashSet<AuthorizeNettransaction>();
        }

        public long CustomerProfileId { get; set; }
        public DateTime CreatedDate { get; set; }
        public long PaymentProfileId { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<AuthorizeNettransaction> AuthorizeNettransaction { get; set; }
        public virtual Users User { get; set; }
    }
}
