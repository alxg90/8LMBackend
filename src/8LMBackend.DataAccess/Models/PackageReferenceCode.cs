using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PackageReferenceCode
    {
        public int PackageId { get; set; }
        public string ReferenceCode { get; set; }
        public bool IsFixed { get; set; }
        public int Value { get; set; }

        public virtual Package Package { get; set; }
    }
}
