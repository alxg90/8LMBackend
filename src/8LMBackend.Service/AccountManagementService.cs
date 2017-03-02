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

		public AccountManagementViewModel AccountList(string access_token)
		{
            VerifyFunction(9, access_token);

			AccountManagementViewModel result = new AccountManagementViewModel();
			
			foreach (var u in DbContext.Users.Include(r => r.Securityrole).Include(t => t.Type).ToList())
			{
				AccountViewModel account = new AccountViewModel()
				{
					id = u.Id,
					login = u.Login,
					typeID = u.TypeId,
                    typeName = u.Type.Name
				};

				account.roles = u.Securityrole.Select(p => p.Id).ToList();
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

		public void AssignFunction(int FunctionID, int RoleID, string access_token)
		{
            VerifyFunction(10, access_token);

            var rf = DbContext.Rolefunction.Where(p => p.RoleId == RoleID && p.FunctionId == FunctionID).FirstOrDefault();
			if (rf == default(Rolefunction))
			{
				Rolefunction item = new Rolefunction()
				{
					RoleId = RoleID,
					FunctionId = FunctionID,
					CreatedBy = GetUserID(access_token),
					CreatedDate = DateTime.UtcNow
				};

				DbContext.Set<Rolefunction>().Add(item);
				DbContext.SaveChanges();
			}
		}

		public void DeassignFunction(int FunctionID, int RoleID, string access_token)
		{
            VerifyFunction(10, access_token);

            var rf = DbContext.Rolefunction.Where(p => p.RoleId == RoleID && p.FunctionId == FunctionID).FirstOrDefault();
			if (rf != default(Rolefunction))
			{
				DbContext.Set<Rolefunction>().Remove(rf);
				DbContext.SaveChanges();
			}
		}

        public void UpdateCode(int yyyy, int mm, string Code, string access_token)
        {
            VerifyFunction(12, access_token);

            if (!DbContext.Promocode.Where(p => p.Code == Code).Any())
            {
                var item = DbContext.Promocode.Where(p => p.Yyyy == yyyy && p.Mm == mm).FirstOrDefault();
                if (item == default(Promocode))
                {
                    Promocode pc = new Promocode()
                    {
                        Yyyy = yyyy,
                        Mm = mm,
                        Code = Code
                    };
                    DbContext.Set<Promocode>().Add(pc);
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

        public List<Promocode> GetCodes(string access_token)
        {
            VerifyFunction(11, access_token);

            return DbContext.Promocode.OrderBy(y => y.Yyyy).OrderBy(m => m.Mm).ToList();
        }

        public void DeletePromoCode(int yyyy, int mm, string access_token)
        {
            VerifyFunction(12, access_token);

            var item = DbContext.Promocode.Where(p => p.Yyyy == yyyy && p.Mm == mm).FirstOrDefault();
            if (item != default(Promocode))
            {
                DbContext.Set<Promocode>().Remove(item);
                DbContext.SaveChanges();
            }
            else
                throw new Exception("Promocode not found");
        }

        public void CodesBulkUpdate(List<Promocode> codes, string access_token)
        {
            foreach (var c in codes)
                UpdateCode(c.Yyyy, c.Mm, c.Code, access_token);
        }

        public List<PromoUserViewModel> GetPromoUsers(int TypeID, string access_token)
        {
            VerifyFunction(11, access_token);

            List<PromoUserViewModel> result = new List<PromoUserViewModel>();
            foreach (var u in DbContext.Users.Where(p => p.TypeId == TypeID).OrderBy(p => p.Id).ToList())
            {
                PromoUserViewModel item = new PromoUserViewModel()
                {
                    id = u.Id
                };
                result.Add(item);
            }

            foreach (var u in result)
            {
                foreach (var c in DbContext.Usercontact.Where(p => p.UserId == u.id).ToList())
                {
                    switch (c.ContactTypeId)
                    {
                        case 1:
                            u.name = c.Value;
                            break;

                        case 2:
                            u.address = c.Value;
                            break;

                        case 3:
                            u.tollFree = c.Value;
                            break;

                        case 4:
                            u.fax = c.Value;
                            break;

                        case 5:
                            u.ordersFax = c.Value;
                            break;

                        case 6:
                            u.email = c.Value;
                            break;

                        case 7:
                            u.ordersEmail = c.Value;
                            break;

                        case 8:
                            u.artworkEmail = c.Value;
                            break;

                        case 9:
                            u.web = c.Value;
                            break;

                        case 10:
                            u.discountPolicy = c.Value;
                            break;

                        case 11:
                            u.customCode = c.Value;
                            break;
                    }
                }

                foreach (var pp in DbContext.Promoproduct.Where(p => p.UserId == u.id).ToList())
                    u.products.Add(pp.Name);
            }

            return result;
        }

        public void UpdatePromoUser(PromoUserViewModel u, string access_token)
        {
            VerifyFunction(12, access_token);

            var item = DbContext.Users.Where(p => p.Id == u.id).FirstOrDefault();
            if (item == default(Users))
            {
                throw new Exception("User with ID = " + u.id.ToString() + " not found");
            }

            foreach (var c in DbContext.Usercontact.Where(p => p.UserId == u.id).ToList())
                DbContext.Set<Usercontact>().Remove(c);

            foreach (var pp in DbContext.Promoproduct.Where(p => p.UserId == u.id).ToList())
                DbContext.Set<Promoproduct>().Remove(pp);

            foreach (var pp in u.products)
            {
                Promoproduct product = new Promoproduct()
                {
                    UserId = u.id,
                    Name = pp
                };
                DbContext.Set<Promoproduct>().Add(product);
            }

            DbContext.Set<Usercontact>().Add(new Usercontact() { UserId = u.id, ContactTypeId = 1, Value = u.name });
            DbContext.Set<Usercontact>().Add(new Usercontact() { UserId = u.id, ContactTypeId = 2, Value = u.address });
            DbContext.Set<Usercontact>().Add(new Usercontact() { UserId = u.id, ContactTypeId = 3, Value = u.tollFree });
            DbContext.Set<Usercontact>().Add(new Usercontact() { UserId = u.id, ContactTypeId = 4, Value = u.fax });
            DbContext.Set<Usercontact>().Add(new Usercontact() { UserId = u.id, ContactTypeId = 5, Value = u.ordersFax });
            DbContext.Set<Usercontact>().Add(new Usercontact() { UserId = u.id, ContactTypeId = 6, Value = u.email });
            DbContext.Set<Usercontact>().Add(new Usercontact() { UserId = u.id, ContactTypeId = 7, Value = u.ordersEmail });
            DbContext.Set<Usercontact>().Add(new Usercontact() { UserId = u.id, ContactTypeId = 8, Value = u.artworkEmail });
            DbContext.Set<Usercontact>().Add(new Usercontact() { UserId = u.id, ContactTypeId = 9, Value = u.web });
            DbContext.Set<Usercontact>().Add(new Usercontact() { UserId = u.id, ContactTypeId = 10, Value = u.discountPolicy });
            DbContext.Set<Usercontact>().Add(new Usercontact() { UserId = u.id, ContactTypeId = 11, Value = u.customCode });

            DbContext.SaveChanges();
        }

        public List<int> GetFunctionsForUser(string access_token)
        {
            return DbContext.Userrole.Where(p => p.UserId == GetUserID(access_token)).Include(p => p.Role).ToList().Select(p => p.RoleId).Distinct().ToList();
        }

        public void CreateSecurityRole(string Name, string Description, string access_token)
        {
            VerifyFunction(10, access_token);

            var item = DbContext.Securityrole.Where(p => p.Name.ToUpper() == Name.ToUpper()).FirstOrDefault();
            if (item == default(Securityrole))
            {
                Securityrole sr = new Securityrole()
                {
                    Name = Name,
                    Description = Description,
                    IsActual = true,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = 1
                };
                DbContext.Set<Securityrole>().Add(sr);
            }
            else
                item.IsActual = true;

            DbContext.SaveChanges();
        }

        public void UpdateSecurityRole(int ID, string Name, string Description, string access_token)
        {
            VerifyFunction(10, access_token);

            var item = DbContext.Securityrole.Where(p => p.Id == ID).FirstOrDefault();
            if (item == default(Securityrole))
                throw new Exception("Role with ID = " + ID.ToString() + " not found");

            var r = DbContext.Securityrole.Where(p => p.Id != ID && p.Name.ToUpper() == Name.ToUpper()).FirstOrDefault();
            if (r == default(Securityrole))
                throw new Exception("Role with Name = '" + Name.ToString() + "' already exists");

            item.Name = Name;
            item.Description = Description;
            DbContext.SaveChanges();
        }

        public void DeleteSecurityRole(int ID, string access_token)
        {
            VerifyFunction(10, access_token);

            var item = DbContext.Securityrole.Where(p => p.Id == ID).Include(p => p.Rolefunction).Include(p => p.Userrole).FirstOrDefault();
            if (item == default(Securityrole))
                throw new Exception("Role with ID = " + ID.ToString() + " not found");

            if ((item.Rolefunction.Count == 0) && (item.Userrole.Count == 0))
                DbContext.Set<Securityrole>().Remove(item);
            else
                item.IsActual = false;

            DbContext.SaveChanges();
        }

        public void AssignRole(int UserID, int RoleID, string access_token)
        {
            VerifyFunction(10, access_token);

            var item = DbContext.Userrole.Where(p => p.UserId == UserID && p.RoleId == RoleID).FirstOrDefault();
            if (item == default(Userrole))
            {
                var ur = new Userrole()
                {
                    UserId = UserID,
                    RoleId = RoleID
                };
                DbContext.Set<Userrole>().Add(ur);
                DbContext.SaveChanges();
            }
        }

        public void DeassignRole(int UserID, int RoleID, string access_token)
        {
            VerifyFunction(10, access_token);

            var item = DbContext.Userrole.Where(p => p.UserId == UserID && p.RoleId == RoleID).FirstOrDefault();
            if (item != default(Userrole))
            {
                DbContext.Set<Userrole>().Remove(item);
                DbContext.SaveChanges();
            }
        }

        public int GetUserID(string access_token)
        {
            var u = DbContext.Usertoken.Where(p => p.Token == access_token).FirstOrDefault();
            if (u == default(Usertoken))
                throw new Exception("Not authorized");

            return u.UserId;
        }

        public void VerifyFunction(int FunctionID, string access_token)
        {
            var fs = GetFunctionsForUser(access_token);
            if (!fs.Contains(FunctionID))
                throw new Exception("Access denied");
        }
    }
}
