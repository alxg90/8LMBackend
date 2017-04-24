using System;
using System.Collections.Generic;
using System.Linq;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using _8LMBackend.DataAccess.Models;
using _8LMBackend.DataAccess.DtoModels;

namespace _8LMBackend.Service
{
    public class AccountManagementService : ServiceBase, IAccountManagementService
    {
		public AccountManagementService(IDbFactory dbFactory)
			: base(dbFactory) 
		{
		}

		public AccountManagementViewModel AccountList(string access_token)
		{
            VerifyFunction(9, access_token);

			AccountManagementViewModel result = new AccountManagementViewModel();
			
			foreach (var u in DbContext.Users.Include(r => r.UserRoleUser).Include(t => t.Type).ToList())
			{
				AccountViewModel account = new AccountViewModel()
				{
					id = u.Id,
					login = u.Login,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    ClearPassword = u.ClearPassword,
                    Email = u.Email,
					typeID = u.TypeId,
                    typeName = u.Type.Name,
                    Icon = u.Icon
				};

				account.roles = u.UserRoleUser.Select(p => p.RoleId).ToList();
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

        public AccountViewModel GetAccount(int id, string token)
        {
            VerifyFunction(9, token);

            var account = DbContext.Users.FirstOrDefault(u => u.Id == id);
            if (account == null)
            {
                return null;
            }
            var accountVM = new AccountViewModel
            {
                id = account.Id,
                login = account.Login,
                FirstName = account.FirstName,
                LastName = account.LastName,
                ClearPassword = account.ClearPassword,
                Email = account.Email,
                typeID = account.TypeId,
                Icon = account.Icon
            };
            return accountVM;
        }

		public void AssignFunction(int FunctionID, int RoleID, string access_token)
		{
            VerifyFunction(10, access_token);

            var rf = DbContext.RoleFunction.Where(p => p.RoleId == RoleID && p.FunctionId == FunctionID).FirstOrDefault();
			if (rf == default(RoleFunction))
			{
				RoleFunction item = new RoleFunction()
				{
					RoleId = RoleID,
					FunctionId = FunctionID,
					CreatedBy = GetUserID(access_token),
					CreatedDate = DateTime.UtcNow
				};

				DbContext.Set<RoleFunction>().Add(item);
				DbContext.SaveChanges();
			}
		}

		public void DeassignFunction(int FunctionID, int RoleID, string access_token)
		{
            VerifyFunction(10, access_token);

            var rf = DbContext.RoleFunction.Where(p => p.RoleId == RoleID && p.FunctionId == FunctionID).FirstOrDefault();
            if (rf != null)
            {
                DbContext.RoleFunction.Remove(rf);
                DbContext.SaveChanges();
            }
        }

        public void UpdateCode(int yyyy, int mm, string Code, string access_token)
        {
            VerifyFunction(12, access_token);

            if (!DbContext.PromoCode.Where(p => p.Code == Code).Any())
            {
                var item = DbContext.PromoCode.Where(p => p.Yyyy == yyyy && p.Mm == mm).FirstOrDefault();
                if (item == default(PromoCode))
                {
                    PromoCode pc = new PromoCode()
                    {
                        Yyyy = yyyy,
                        Mm = mm,
                        Code = Code
                    };
                    DbContext.Set<PromoCode>().Add(pc);
                }
                else
                {
                    item.Code = Code;
                }

                DbContext.SaveChanges();
            }
            else
            {
                throw new Exception("The code '" + Code + "' already exists");
            }
        }

        public List<PromoCode> GetCodes(string access_token)
        {
            VerifyFunction(16, access_token);

            return DbContext.PromoCode.OrderBy(y => y.Yyyy).OrderBy(m => m.Mm).ToList();
        }

        public void DeletePromoCode(int yyyy, int mm, string access_token)
        {
            VerifyFunction(12, access_token);

            var item = DbContext.PromoCode.Where(p => p.Yyyy == yyyy && p.Mm == mm).FirstOrDefault();
            if (item != default(PromoCode))
            {
                DbContext.Set<PromoCode>().Remove(item);
                DbContext.SaveChanges();
            }
            else
                throw new Exception("Promocode not found");
        }

        public void CodesBulkUpdate(List<PromoCode> codes, string access_token)
        {
            foreach (var c in codes)
                UpdateCode(c.Yyyy, c.Mm, c.Code, access_token);
        }

        public List<PromoUserViewModel> GetPromoSuppliers(string access_token)
        {
            VerifyFunction(16, access_token);

            List<PromoUserViewModel> result = new List<PromoUserViewModel>();
            foreach (var u in DbContext.PromoSupplier.OrderBy(p => p.Id).ToList())
            {
                PromoUserViewModel item = new PromoUserViewModel()
                {
                    id = u.Id,
                    name = u.Name,
                    address = u.Address,
                    tollFree = u.Tollfree,
                    fax = u.Fax,
                    ordersFax = u.OrdersFax,
                    email = u.Email,
                    ordersEmail = u.OrdersEmail,
                    artworkEmail = u.ArtworkEmail,
                    web = u.Web,
                    discountPolicy = u.DiscountPolicy,
                    customCode = u.CustomCode
                };
                result.Add(item);
            }
            foreach (var u in result)
                foreach (var pp in DbContext.PromoProduct.Where(p => p.Id == u.id).ToList())
                    u.products.Add(pp.Name);

            return result;
        }

        public void UpdatePromoUser(PromoUserViewModel u, string access_token)
        {
            VerifyFunction(12, access_token);

            bool isNew = false;
            var item = DbContext.PromoSupplier.Where(p => p.Id == u.id).FirstOrDefault();
            if (item == default(PromoSupplier))
            {
                item = new PromoSupplier();
                isNew = true;
            }

            item.Name = u.name;
            item.Address = u.address;
            item.Tollfree = u.tollFree;
            item.Fax = u.fax;
            item.OrdersFax = u.ordersFax;
            item.Email = u.email;
            item.OrdersEmail = u.ordersEmail;
            item.ArtworkEmail = u.artworkEmail;
            item.Web = u.web;
            item.DiscountPolicy = u.discountPolicy;
            item.CustomCode = u.customCode;

            if (isNew)
                DbContext.Set<PromoSupplier>().Add(item);

            if (!isNew)
                foreach (var pp in DbContext.PromoProduct.Where(p => p.Id == u.id).ToList())
                    DbContext.Set<PromoProduct>().Remove(pp);

            foreach (var pp in u.products)
            {
                PromoProduct npp = new PromoProduct()
                {
                    Id = u.id,
                    Name = pp
                };
                DbContext.Set<PromoProduct>().Add(npp);
            }

            DbContext.SaveChanges();
        }

        public List<int> GetFunctionsForUser(string access_token)
        {
            var userId = GetUserID(access_token);
            if(DbContext.Users.FirstOrDefault(x => x.Id == userId).TypeId == Types.Users.Admin){
                return DbContext.SecurityFunction.Select(sf => sf.Id).ToList();
            }
            var result = DbContext.UserRole.Where(p => p.UserId == userId)
                .Include(p => p.Role)
                .Join(DbContext.RoleFunction, p => p.RoleId, rf => rf.RoleId, (p, rf) => rf.FunctionId)
                .Distinct().ToList();

            var res = DbContext.Subscription.Include("PackageRatePlan")
                        .Where(p => p.UserId == userId 
                            && p.StatusId == Statuses.Subscription.Active 
                            && p.EffectiveDate.Date <= DateTime.UtcNow.Date
                            && p.ExpirationDate.Date >= DateTime.UtcNow.Date)
                        .Join(DbContext.PackageService, s => s.PackageRatePlan.PackageId, ps => ps.PackageId, (s, ps) => ps)
                        .Join(DbContext.ServiceFunction, ps => ps.ServiceId, sf => sf.ServiceId, (ps, sf) => sf)
                        .Select(sf => sf.SecurityFunctionId).ToList();

            result.AddRange(res);

            return result.Distinct().ToList();
        }

        public int CreateSecurityRole(string Name, string Description, string access_token)
        {
            VerifyFunction(10, access_token);

            var item = DbContext.SecurityRole.Where(p => p.Name.ToUpper() == Name.ToUpper()).FirstOrDefault();
            if (item == default(SecurityRole))
            {
                SecurityRole sr = new SecurityRole()
                {
                    Name = Name,
                    Description = Description,
                    IsActual = true,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = 1
                };
                DbContext.Set<SecurityRole>().Add(sr);
            }
            else
                item.IsActual = true;

            DbContext.SaveChanges();
            return item.Id;
        }

        public void UpdateSecurityRole(int ID, string Name, string Description, string access_token)
        {
            VerifyFunction(10, access_token);

            var item = DbContext.SecurityRole.Where(p => p.Id == ID).FirstOrDefault();
            if (item == default(SecurityRole))
                throw new Exception("Role with ID = " + ID.ToString() + " not found");

            var r = DbContext.SecurityRole.Where(p => p.Id != ID && p.Name.ToUpper() == Name.ToUpper()).FirstOrDefault();
            if (r != default(SecurityRole))
                throw new Exception("Role with Name = '" + Name.ToString() + "' already exists");

            item.Name = Name;
            item.Description = Description;
            DbContext.SaveChanges();
        }

        public void DeleteSecurityRole(int ID, string access_token)
        {
            VerifyFunction(10, access_token);

            var item = DbContext.SecurityRole.Where(p => p.Id == ID).Include(p => p.RoleFunction).Include(p => p.UserRole).FirstOrDefault();
            // var item = DbContext.SecurityRole.FirstOrDefault(p => p.Id == ID);
            if (item == default(SecurityRole))
                throw new Exception("Role with ID = " + ID.ToString() + " not found");

            DbContext.SecurityRole.Remove(item);

            if ((item.RoleFunction.Count == 0) && (item.UserRole.Count == 0))
               DbContext.SecurityRole.Remove(item);
            else
               item.IsActual = false;

            DbContext.SaveChanges();
        }

        public void AssignRole(int UserID, int RoleID, string access_token)
        {
            VerifyFunction(10, access_token);

            var item = DbContext.UserRole.Where(p => p.UserId == UserID && p.RoleId == RoleID).FirstOrDefault();
            if (item == default(UserRole))
            {
                var ur = new UserRole()
                {
                    UserId = UserID,
                    RoleId = RoleID
                };
                DbContext.Set<UserRole>().Add(ur);
                DbContext.SaveChanges();
            }
        }

        public void DeassignRole(int UserID, int RoleID, string access_token)
        {
            VerifyFunction(10, access_token);

            var item = DbContext.UserRole.Where(p => p.UserId == UserID && p.RoleId == RoleID).FirstOrDefault();
            if (item != default(UserRole))
            {
                DbContext.Set<UserRole>().Remove(item);
                DbContext.SaveChanges();
            }
        }

        public int GetUserID(string access_token)
        {
            var u = DbContext.UserToken.Where(p => p.Token == access_token).FirstOrDefault();
            if (u == default(UserToken))
                throw new Exception("Not authorized");

            return u.UserId;
        }

        public void VerifyFunction(int FunctionID, string access_token)
        {
            var fs = GetFunctionsForUser(access_token);
            if (!fs.Contains(FunctionID))
                throw new Exception("Access denied");
        }

        public void DeletePromoUser(int ID, string token)
        {
            VerifyFunction(12, token);

            foreach (var pp in DbContext.PromoProduct.Where(p => p.Id == ID).ToList())
                DbContext.Set<PromoProduct>().Remove(pp);

            var item = DbContext.PromoSupplier.Where(p => p.Id == ID).FirstOrDefault();
            if (item != default(PromoSupplier))
            {
                DbContext.Set<PromoSupplier>().Remove(item);
                DbContext.SaveChanges();
            }
        }

        public void UpdateUser(Users u, string token)
        {
            VerifyFunction(10, token);

            var item = DbContext.Users.Where(p => p.Id == u.Id).FirstOrDefault();
            if (item != default(Users))
            {
                item.FirstName = u.FirstName;
                item.LastName = u.LastName;
                item.ClearPassword = u.ClearPassword;
                item.Email = u.Email;
                item.Icon = u.Icon;
                DbContext.SaveChanges();
            }
            else
                throw new Exception("User with ID = " + u.Id.ToString() + " not found");

        }

        public void DeleteUser(Users u, string token)
        {
            VerifyFunction(10, token);

            var item = DbContext.Users.Where(p => p.Id == u.Id).FirstOrDefault();
            if (item != default(Users))
            {
                DbContext.SaveChanges();
                DbContext.Set<Users>().Remove(item);
            }
            else
                throw new Exception("User with ID = " + u.Id.ToString() + " not found");

        }
    }
}
