using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _8LMBackend.DataAccess.Infrastructure;

namespace _8LMBackend.Service.ViewModels
{
    public class RoleViewModel
    {
		public int  id { get; set; }
		public string name { get; set; }

		public List<int> functions;
    }
}
