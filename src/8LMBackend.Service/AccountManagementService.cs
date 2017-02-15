using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.Service
{
    public class AccountManagementService : ServiceBase, IAccountManagementService
    {
		public AccountManagementService(IDbFactory dbFactory)
			: base(dbFactory) 
		{
		}

		public AccountManagementViewModel AccountList()
		{
			AccountManagementViewModel result = new AccountManagementViewModel();
			
			foreach (var u in DbContext.Users.Include(r => r.SecurityRole).ToList())
			{
				AccountViewModel account = new AccountViewModel()
				{
					id = u.Id,
					firstName = u.Login,
					lastName = u.Login//,
					//email = u.email,
					//icon = u.icon
				};

				account.roles = u.SecurityRole.Select(p => p.Id).ToList();
				result.accounts.Add(account);
			}

			foreach (var r in DbContext.SecurityRole.Include(f => f.RoleFunction).ToList())
			{
				RoleViewModel role = new RoleViewModel()
				{
					id = r.Id,
					name = r.Name
				};

				role.functions = r.RoleFunction.Select(p => p.FunctionId).ToList();
				result.roles.Add(role); 
			}

			result.securityFunctions = new List<SecurityFunctionViewModel>();
			foreach (var f in DbContext.SecurityFunction.ToList())
			{
				SecurityFunctionViewModel function = new SecurityFunctionViewModel()
				{
					id = f.Id,
					name = f.Name
				};

				result.securityFunctions.Add(function);
			}

			return result;
		}

		public void AssignFunction(int FunctionID, int RoleID, int CreatedBy)
		{
			var rf = DbContext.RoleFunction.Where(p => p.RoleId == RoleID && p.FunctionId == FunctionID).FirstOrDefault();
			if (rf == default(RoleFunction))
			{
				RoleFunction item = new RoleFunction()
				{
					RoleId = RoleID,
					FunctionId = FunctionID,
					CreatedBy = CreatedBy,
					CreatedDate = DateTime.UtcNow
				};

				DbContext.Set<RoleFunction>().Add(item);
				DbContext.SaveChanges();
			}
		}

		public void DeassignFunction(int FunctionID, int RoleID)
		{
			var rf = DbContext.RoleFunction.Where(p => p.RoleId == RoleID && p.FunctionId == FunctionID).FirstOrDefault();
			if (rf != default(RoleFunction))
			{
				DbContext.Set<RoleFunction>().Remove(rf);
				DbContext.SaveChanges();
			}
		}
    }
}
