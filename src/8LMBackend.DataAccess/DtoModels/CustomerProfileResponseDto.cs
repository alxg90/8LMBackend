using System;
using System.Collections.Generic;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.DataAccess.DtoModels
{
    public partial class CustomerProfileResponseDto
    {
        public CustomerProfileResponseDto()
        {

        }
        public int customerProfileId { get; set; }
        public int[] customerPaymentProfileIdList { get; set; }
        public int[] customerShippingAddressIdList { get; set; }
        public int[] validationDirectResponseList { get; set; }
        public MessagesDto messages { get; set; }
    }
    public class MessagesDto{
        public string resultCode { get; set; }
        public Dictionary<string,string>[] message { get; set; }                                       
    }
}
