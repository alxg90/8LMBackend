using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _8LMBackend.DataAccess.Infrastructure;

namespace _8LMBackend.Service.ViewModels
{
    public class AccountManagementViewModel
    {
		public List<AccountViewModel> accounts = new List<AccountViewModel>();
		public List<RoleViewModel> roles = new List<RoleViewModel>();
		public List<SecurityFunctionViewModel> securityFunctions = new List<SecurityFunctionViewModel>();
    }
}
