using System;
using System.Collections.Generic;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.DataAccess.DtoModels
{
    public partial class CreateTransactionResponseDto
    {
        public CreateTransactionResponseDto()
        {

        }
        public TransResposneDto transactionResponse { get; set; }
        public MessagesDto messages { get; set; }
    }
    public class TransMessagesDto{
        public int? code { get; set; }
        public string description { get; set; }                                       
    }
    public class TransProfileDto{
        public string customerProfileId { get; set; }
        public string customerPaymentProfileId { get; set; }
    }
    public class TransResposneDto{
        public int? responseCode { get; set; }
        public int? authCode { get; set; }
        public string avsResultCode { get; set; }
        public string cvvResultCode { get; set; }
        public string cavvResultCode { get; set; }
        public string transId { get; set; }
        public string refTransID { get; set; }
        public string transHash { get; set; }
        public int? testRequest { get; set; }
        public string accountNumber { get; set; }
        public string accountType { get; set; }
        public TransMessagesDto messages { get; set; }
        public string transHashSha2 { get; set; }
    }
}
