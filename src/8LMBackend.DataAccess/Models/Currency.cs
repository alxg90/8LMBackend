using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Currency
    {
        public Currency()
        {
            PackageRatePlan = new HashSet<PackageRatePlan>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Mark { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PackageRatePlan> PackageRatePlan { get; set; }
    }
}
