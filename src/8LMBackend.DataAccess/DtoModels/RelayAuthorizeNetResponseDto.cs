using System;
using System.Collections.Generic;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.DataAccess.DtoModels
{
    public partial class RelayAuthorizeNetresponseDto
    {
        public string x_account_number { get; set; }
        public string x_address { get; set; }
        public string x_amount { get; set; }
        public int x_auth_code { get; set; }
        public string x_avs_code { get; set; }
        public string x_card_type { get; set; }
        public string x_cavv_response { get; set; }
        public string x_city { get; set; }
        public string x_company { get; set; }
        public string x_country { get; set; }
        public string x_cust_id { get; set; }
        public string x_cvv2_resp_code { get; set; }
        public string x_description { get; set; }
        public string x_duty { get; set; }
        public string x_email { get; set; }
        public string x_fax { get; set; }
        public string x_first_Name { get; set; }
        public string x_freight { get; set; }
        public string x_invoice_num { get; set; }
        public string x_last_name { get; set; }
        public string x_MD5_Hash { get; set; }
        public string x_method { get; set; }
        public string x_phone { get; set; }
        public string x_po_num { get; set; }
        public int x_response_code { get; set; }
        public int x_response_reason_code { get; set; }
        public string x_response_reason_text { get; set; }
        public string x_SHA2_Hash { get; set; }
        public string x_ship_to_address { get; set; }
        public string x_ship_to_city { get; set; }
        public string x_ship_to_company { get; set; }
        public string x_ship_to_country { get; set; }
        public string x_ship_to_first_name { get; set; }
        public string x_ship_to_last_name { get; set; }
        public string x_ship_to_state { get; set; }
        public string x_ship_to_zip { get; set; }
        public string x_state { get; set; }
        public string x_tax { get; set; }
        public string x_tax_exempt { get; set; }
        public string x_test_request { get; set; }
        public long x_trans_id { get; set; }
        public string x_type { get; set; }
        public string x_zip { get; set; }
    }
}
