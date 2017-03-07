
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClearPassword { get; set; }
        public string Email { get; set; }
        public string Icon { get; set; }
        public int typeID { get; set; }
		public string typeName { get; set; }

        public List<int> roles;
    }
}
