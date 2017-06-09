using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            AuthorizeNettransaction = new HashSet<AuthorizeNettransaction>();
            RelayAuthorizeNetresponse = new HashSet<RelayAuthorizeNetresponse>();
        }

        public int Id { get; set; }
        public int Amount { get; set; }
        public int AmountDue { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Discount { get; set; }
        public int PackageRatePlanId { get; set; }
        public string ReferenceCode { get; set; }
        public int StatusId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<AuthorizeNettransaction> AuthorizeNettransaction { get; set; }
        public virtual ICollection<RelayAuthorizeNetresponse> RelayAuthorizeNetresponse { get; set; }
        public virtual PackageRatePlan PackageRatePlan { get; set; }
        public virtual EntityStatus Status { get; set; }
        public virtual Users User { get; set; }
    }
}
