using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Service
    {
        public Service()
        {
            RoleService = new HashSet<RoleService>();
            PackageReferenceServiceCode = new HashSet<PackageReferenceServiceCode>();
            PackageService = new HashSet<PackageService>();
            ServiceFunction = new HashSet<ServiceFunction>();
            SubscriptionExtraService = new HashSet<SubscriptionExtraService>();
        }

        public int Id { get; set; }
        public bool IsActual { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RoleService> RoleService { get; set; }
        public virtual ICollection<PackageReferenceServiceCode> PackageReferenceServiceCode { get; set; }
        public virtual ICollection<PackageService> PackageService { get; set; }
        public virtual ICollection<ServiceFunction> ServiceFunction { get; set; }
        public virtual ICollection<SubscriptionExtraService> SubscriptionExtraService { get; set; }
    }
}
