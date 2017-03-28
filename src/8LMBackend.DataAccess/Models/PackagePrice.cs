using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PackagePrice
    {
        public int PackageId { get; set; }
        public int CurrencyId { get; set; }
        public int DurationInMonth { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? IsActual { get; set; }
        public int Price { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
