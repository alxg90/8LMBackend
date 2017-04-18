using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class AuthorizeNettransaction
    {
        public long Id { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CustomerProfileId { get; set; }
        public string Description { get; set; }
        public int InvoiceId { get; set; }
        public string MerchantName { get; set; }
        public string MerchantTransactionKey { get; set; }
        public long PaymentProfileId { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseResultCode { get; set; }
        public string ResponseText { get; set; }
        public string TransactionResponseAccountNumber { get; set; }
        public string TransactionResponseAccountType { get; set; }
        public int? TransactionResponseAuthCode { get; set; }
        public string TransactionResponseAvsresultCode { get; set; }
        public string TransactionResponseCavvresultCode { get; set; }
        public string TransactionResponseCvvresultCode { get; set; }
        public int? TransactionResponseMessageCode { get; set; }
        public string TransactionResponseMessageDescription { get; set; }
        public string TransactionResponseRefTransId { get; set; }
        public int? TransactionResponseResponseCode { get; set; }
        public int? TransactionResponseTestRequest { get; set; }
        public string TransactionResponseTransHash { get; set; }
        public string TransactionResponseTransId { get; set; }
        public string TransactionType { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual AuthorizeNetcustomerProfile CustomerProfile { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
