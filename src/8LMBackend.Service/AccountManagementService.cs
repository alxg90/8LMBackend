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
			
			foreach (var u in DbContext.Users.Include(r => r.Securityrole).Include(c => c.Userpromocode).ToList())
			{
				AccountViewModel account = new AccountViewModel()
				{
					id = u.Id,
					firstName = u.Login,
					lastName = u.Login//,
					//email = u.email,
					//icon = u.icon
				};

				account.roles = u.Securityrole.Select(p => p.Id).ToList();
                account.PromoCode = u.Userpromocode.Where(p => p.IsActive).FirstOrDefault().Code;
				result.accounts.Add(account);
			}

			foreach (var r in DbContext.Securityrole.Include(f => f.Rolefunction).ToList())
			{
				RoleViewModel role = new RoleViewModel()
				{
					id = r.Id,
					name = r.Name
				};

				role.functions = r.Rolefunction.Select(p => p.FunctionId).ToList();
				result.roles.Add(role); 
			}

			result.securityFunctions = new List<SecurityFunctionViewModel>();
			foreach (var f in DbContext.Securityfunction.ToList())
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
			var rf = DbContext.Rolefunction.Where(p => p.RoleId == RoleID && p.FunctionId == FunctionID).FirstOrDefault();
			if (rf == default(Rolefunction))
			{
				Rolefunction item = new Rolefunction()
				{
					RoleId = RoleID,
					FunctionId = FunctionID,
					CreatedBy = CreatedBy,
					CreatedDate = DateTime.UtcNow
				};

				DbContext.Set<Rolefunction>().Add(item);
				DbContext.SaveChanges();
			}
		}

		public void DeassignFunction(int FunctionID, int RoleID)
		{
			var rf = DbContext.Rolefunction.Where(p => p.RoleId == RoleID && p.FunctionId == FunctionID).FirstOrDefault();
			if (rf != default(Rolefunction))
			{
				DbContext.Set<Rolefunction>().Remove(rf);
				DbContext.SaveChanges();
			}
		}

        public void AddPromoCode(string Code, int dtFrom, int dtTo)
        {
            if(!DbContext.Promocode.Where(p => p.Code == Code).Any())
            {
                Promocode pc = new Promocode()
                {
                    Code = Code,
                    FromDate = dtFrom,
                    ToDate = dtTo
                };
                DbContext.Set<Promocode>().Add(pc);
                DbContext.SaveChanges();
            }
            else
            {
                throw new Exception("This code already exists");
            }
        }

        public void AssignPromoCode(int UserID, string Code)
        {
            foreach (var item in DbContext.Userpromocode.Where(p => p.UserId == UserID && p.IsActive))
                item.IsActive = false;

            Userpromocode upc = new Userpromocode()
            {
                UserId = UserID,
                Code = Code,
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            };

            DbContext.Set<Userpromocode>().Add(upc);
            DbContext.SaveChanges();
        }

        public void DeassignPromoCode(int UserID)
        {
            foreach (var item in DbContext.Userpromocode.Where(p => p.UserId == UserID && p.IsActive))
                item.IsActive = false;

            DbContext.SaveChanges();
        }

        public List<Promocode> CodeList()
        {
            return DbContext.Promocode.OrderBy(p => p.FromDate).ToList();
        }

        public void UpdatePromoCode(int ID, string Code, int FromDate, int ToDate)
        {
            var item = DbContext.Promocode.Where(p => p.Id == ID).FirstOrDefault();
            if (item != default(Promocode))
            {
                item.Code = Code;
                item.FromDate = FromDate;
                item.ToDate = ToDate;
                DbContext.SaveChanges();
            }
            else
                throw new Exception("Promocode not found");
        }

        public void DeletePromoCode(int ID)
        {
            var item = DbContext.Promocode.Where(p => p.Id == ID).FirstOrDefault();
            if (item != default(Promocode))
            {
                DbContext.Set<Promocode>().Remove(item);
                DbContext.SaveChanges();
            }
            else
                throw new Exception("Promocode not found");
        }
    }
}
