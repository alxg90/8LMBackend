using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Promoproduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SupplierId { get; set; }

        public virtual Promosupplier Supplier { get; set; }
    }
}
