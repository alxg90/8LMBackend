using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PackageReferenceExtendCode
    {
        public int PackageId { get; set; }
        public string ReferenceCode { get; set; }
        public int Months { get; set; }

        public virtual Package Package { get; set; }
    }
}
