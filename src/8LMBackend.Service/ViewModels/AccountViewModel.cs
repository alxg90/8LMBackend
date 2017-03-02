
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _8LMBackend.DataAccess.Infrastructure;

namespace _8LMBackend.Service.ViewModels
{
    public class AccountViewModel
    {
		public int id { get; set; }
		public string login { get; set; }
		public int typeID { get; set; }
		public string typeName { get; set; }

        public List<int> roles;
    }
}
