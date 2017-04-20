using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Package
    {
        public Package()
        {
            Invoice = new HashSet<Invoice>();
            PackageRatePlan = new HashSet<PackageRatePlan>();
            PackageService = new HashSet<PackageService>();
            Subscription = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int PaletteId { get; set; }
        public int StatusId { get; set; }
        public int UserTypeId { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<PackageRatePlan> PackageRatePlan { get; set; }
        public virtual ICollection<PackageService> PackageService { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
        public virtual Users CreatedByNavigation { get; set; }
        public virtual EntityStatus Status { get; set; }
        public virtual EntityType UserType { get; set; }
    }
}
