using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _8LMBackend.DataAccess.Infrastructure;

namespace _8LMBackend.Service.ViewModels
{
    public class AccountManagementViewModel
    {
		public List<AccountViewModel> accounts;
		public List<RoleViewModel> roles;
		public List<SecurityFunctionViewModel> securityFunctions;
    }
}
