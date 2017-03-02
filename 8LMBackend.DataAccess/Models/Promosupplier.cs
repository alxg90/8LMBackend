using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Promosupplier
    {
        public Promosupplier()
        {
            Promoproduct = new HashSet<Promoproduct>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public string ArtworkEmail { get; set; }
        public string CustomCode { get; set; }
        public string DiscountPolicy { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Name { get; set; }
        public string OrdersEmail { get; set; }
        public string OrdersFax { get; set; }
        public string Tollfree { get; set; }
        public string Web { get; set; }

        public virtual ICollection<Promoproduct> Promoproduct { get; set; }
    }
}
