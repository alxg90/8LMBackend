using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Package
    {
        public Package()
        {
            Invoice = new HashSet<Invoice>();
            PackageReferenceCode = new HashSet<PackageReferenceCode>();
            PackageReferenceExtendCode = new HashSet<PackageReferenceExtendCode>();
            PackageReferenceServiceCode = new HashSet<PackageReferenceServiceCode>();
            PackageService = new HashSet<PackageService>();
            Subscription = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CurrencyId { get; set; }
        public string Description { get; set; }
        public int DurationInMonth { get; set; }
        public string Name { get; set; }
        public int PaletteId { get; set; }
        public int Price { get; set; }
        public int StatusId { get; set; }
        public int UserTypeId { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<PackageReferenceCode> PackageReferenceCode { get; set; }
        public virtual ICollection<PackageReferenceExtendCode> PackageReferenceExtendCode { get; set; }
        public virtual ICollection<PackageReferenceServiceCode> PackageReferenceServiceCode { get; set; }
        public virtual ICollection<PackageService> PackageService { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
