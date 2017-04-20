using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PackageReferenceServiceCode
    {
        public int PackageRatePlanId { get; set; }
        public string ReferenceCode { get; set; }
        public int ServiceId { get; set; }

        public virtual PackageRatePlan PackageRatePlan { get; set; }
        public virtual Service Service { get; set; }
    }
}
