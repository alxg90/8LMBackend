using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Service
    {
        public Service()
        {
            PackageReferenceServiceCode = new HashSet<PackageReferenceServiceCode>();
            PackageService = new HashSet<PackageService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActual { get; set; }

        public virtual ICollection<PackageReferenceServiceCode> PackageReferenceServiceCode { get; set; }
        public virtual ICollection<PackageService> PackageService { get; set; }
    }
}
