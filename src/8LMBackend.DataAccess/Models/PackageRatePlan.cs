using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PackageRatePlan
    {
        public PackageRatePlan()
        {
            Invoice = new HashSet<Invoice>();
            PackageReferenceCode = new HashSet<PackageReferenceCode>();
            PackageReferenceExtendCode = new HashSet<PackageReferenceExtendCode>();
            PackageReferenceServiceCode = new HashSet<PackageReferenceServiceCode>();
            Subscription = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public int DurationInMonths { get; set; }
        public int PackageId { get; set; }
        public int Price { get; set; }
        public int EmailLimitBroadcast { get; set; }
        public int EmailLimitAddress { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<PackageReferenceCode> PackageReferenceCode { get; set; }
        public virtual ICollection<PackageReferenceExtendCode> PackageReferenceExtendCode { get; set; }
        public virtual ICollection<PackageReferenceServiceCode> PackageReferenceServiceCode { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Package Package { get; set; }
    }
}
