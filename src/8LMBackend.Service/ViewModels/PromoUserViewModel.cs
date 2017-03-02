
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _8LMBackend.DataAccess.Infrastructure;

namespace _8LMBackend.Service.ViewModels
{
    public class PromoUserViewModel
    {
		public int id { get; set; }
		public string name { get; set; }
		public string address { get; set; }
		public string tollFree { get; set; }
        public string fax { get; set; }
        public string ordersFax { get; set; }
        public string email { get; set; }
        public string ordersEmail { get; set; }
        public string artworkEmail { get; set; }
        public string web { get; set; }
        public string discountPolicy { get; set; }
        public string customCode { get; set; }

        public List<string> products = new List<string>();
    }
}
