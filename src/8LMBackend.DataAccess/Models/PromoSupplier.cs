using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PromoSupplier
    {
        public PromoSupplier()
        {
            PromoProduct = new HashSet<PromoProduct>();
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
        public string notes { get; set; }
        public string externalLink { get; set; }
        public string DocumentPath { get; set; }
        public virtual ICollection<PromoProduct> PromoProduct { get; set; }
        
        /// <summary>
        /// LodoId for Suppplier
        /// </summary>
        public int? LogoID { get; set; }
    }
}
