using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PackageReferenceServiceCode
    {
        public int PackageId { get; set; }
        public string ReferenceCode { get; set; }
        public int ServiceId { get; set; }

        public virtual Package Package { get; set; }
        public virtual Service Service { get; set; }
    }
}
