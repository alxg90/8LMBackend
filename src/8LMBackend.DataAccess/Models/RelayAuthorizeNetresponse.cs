using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class RelayAuthorizeNetresponse
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int InvoiceId { get; set; }
        public string XAccountNumber { get; set; }
        public string XAddress { get; set; }
        public string XAmount { get; set; }
        public int XAuthCode { get; set; }
        public string XAvsCode { get; set; }
        public string XCardType { get; set; }
        public string XCavvResponse { get; set; }
        public string XCity { get; set; }
        public string XCompany { get; set; }
        public string XCountry { get; set; }
        public string XCustId { get; set; }
        public string XCvv2RespCode { get; set; }
        public string XDescription { get; set; }
        public string XDuty { get; set; }
        public string XEmail { get; set; }
        public string XFax { get; set; }
        public string XFirstName { get; set; }
        public string XFreight { get; set; }
        public string XInvoiceNum { get; set; }
        public string XLastName { get; set; }
        public string XMd5Hash { get; set; }
        public string XMethod { get; set; }
        public string XPhone { get; set; }
        public string XPoNum { get; set; }
        public int XResponseCode { get; set; }
        public int XResponseReasonCode { get; set; }
        public string XResponseReasonText { get; set; }
        public string XSha2Hash { get; set; }
        public string XShipToAddress { get; set; }
        public string XShipToCity { get; set; }
        public string XShipToCompany { get; set; }
        public string XShipToCountry { get; set; }
        public string XShipToFirstName { get; set; }
        public string XShipToLastName { get; set; }
        public string XShipToState { get; set; }
        public string XShipToZip { get; set; }
        public string XState { get; set; }
        public string XTax { get; set; }
        public string XTaxExempt { get; set; }
        public string XTestRequest { get; set; }
        public long XTransId { get; set; }
        public string XType { get; set; }
        public string XZip { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
