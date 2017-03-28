using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Package = new HashSet<Package>();
            PackagePrice = new HashSet<PackagePrice>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Mark { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Package> Package { get; set; }
        public virtual ICollection<PackagePrice> PackagePrice { get; set; }
    }
}
