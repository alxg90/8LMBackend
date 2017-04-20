using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class PaymentSetting
    {
        public int Id { get; set; }
        public string AuthorizeNetlogin { get; set; }
        public string AuthorizeNettransactionKey { get; set; }
        public string SignatureKey { get; set; }
        public int WelcomePackageDays { get; set; }
        public int WelcomePackagePrice { get; set; }
    }
}
