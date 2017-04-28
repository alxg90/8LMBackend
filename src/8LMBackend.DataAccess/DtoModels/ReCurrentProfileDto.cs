using System;
using System.Collections.Generic;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.DataAccess.DtoModels
{
    public partial class ReCurrentProfileDto
    {
        public ReCurrentProfileDto()
        {
        }
        public long CustomerProfileId { get; set; }
        public long PaymentProfileId { get; set; }
        public int InvoiceId { get; set; }
        public int SubscriptionId { get; set; }
    }
}
