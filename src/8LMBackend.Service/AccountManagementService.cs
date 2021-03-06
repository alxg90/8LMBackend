﻿using System;
using System.Collections.Generic;
using System.Linq;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using _8LMBackend.DataAccess.Models;
using _8LMBackend.DataAccess.DtoModels;
using System.IO;
using _8LMBackend.DataAccess.Enums;

namespace _8LMBackend.Service
{
    public class AccountManagementService : ServiceBase, IAccountManagementService
    {
        private readonly IFileManagerService _fileManager;
        private readonly IPagesService _pagesService;
		public AccountManagementService(
            IDbFactory dbFactory, 
            IFileManagerService fileManager,
            IPagesService pagesService)
			: base(dbFactory) 
		{
            _fileManager = fileManager;
            _pagesService = pagesService;
		}

        public AccountViewModel GetAccount(string access_token)
        {
            VerifyFunction(SecurityFunctions.Emails_Broadcast, access_token);

            int UID = GetUserID(access_token);

            var d = DevDbContext.distributors.First(p => p.id == UID);
            var u = DbContext.Users.Where(p => p.Id == UID).First();
            AccountViewModel result = new AccountViewModel()
            {
                id = u.Id,
                login = u.Login,
                FirstName = u.FirstName,
                LastName = u.LastName,
                //ClearPassword = u.ClearPassword,
                Email = u.Email,
                typeID = u.TypeId,
                Icon = u.Icon,
                company = d.company,
                phone = d.phone,
                mailing_state = d.mailing_state,
                StatusID = u.StatusId,
                EnrollmentDate = u.CreatedDate,
                mailing_address = d.mailing_address,
                mailing_address2 = d.mailing_address2,
                mailing_city = d.mailing_city,
                mailing_zip = d.mailing_zip,
                mailing_country = d.mailing_country
            };

            return result;
        }

        public void ExcludeEmailAddress(string access_token, string email)
        {
            int UID = GetUserID(access_token);

            ExcludeEmail item = DbContext.ExcludeEmail.FirstOrDefault(p => p.UserID == UID && p.email.ToUpper() == email.ToUpper());
            if (item == null)
            {
                item = new ExcludeEmail();
                item.UserID = UID;
                item.email = email;
                item.CreatedDate = DateTime.UtcNow;
                DbContext.Set<ExcludeEmail>().Add(item);
                DbContext.SaveChanges();
            }
        }

        public List<AccountViewModel> AccountList(string access_token)
		{
            VerifyFunction(SecurityFunctions.Emails_Broadcast, access_token);

			var result = new List<AccountViewModel>();
            var distributors = DevDbContext.distributors;

            Dictionary<int, string> dCompany = new Dictionary<int, string>();
            Dictionary<int, string> dPhone = new Dictionary<int, string>();
            Dictionary<int, string> dMailing_state = new Dictionary<int, string>();

            foreach (var d in distributors)
            {
                dCompany.Add(d.id, d.company);
                dPhone.Add(d.id, d.phone);
                dMailing_state.Add(d.id, d.mailing_state);
            }

            var ServiceDictionary = new List<ServiceViewModel>();
            foreach (var s in DbContext.Service.ToList())
            {
                ServiceViewModel item = new ServiceViewModel()
                {
                    ID = s.Id,
                    Name = s.Name
                };
                ServiceDictionary.Add(item);
            }

            var RoleDictionary = new List<RoleViewModel>();
            foreach (var r in DbContext.SecurityRole.Include(p => p.RoleService).ToList())
            {
                RoleViewModel item = new RoleViewModel()
                {
                    id = r.Id,
                    name = r.Name,
                    Services = new List<ServiceViewModel>()
                };

                foreach (var sid in r.RoleService.Select(s => s.ServiceId).ToList())
                    item.Services.Add(ServiceDictionary.First(s => s.ID == sid));

                RoleDictionary.Add(item);
            }

            var RatePlanDictionary = DbContext.PackageRatePlan.Include(p => p.Package).ToList();
            var PackageServiceDictionary = DbContext.PackageService.ToList();

            foreach (var u in DbContext.Users.Include(r => r.UserRoleUser).Include(t => t.Type).Include(s => s.Status).Include(x => x.Subscription).ToList())
            {
                string _company;
                string _phone;
                string _mailing_state;
                dCompany.TryGetValue(u.Id, out _company);
                dPhone.TryGetValue(u.Id, out _phone);
                dMailing_state.TryGetValue(u.Id, out _mailing_state);

                AccountViewModel account = new AccountViewModel()
                {
                    id = u.Id,
                    login = u.Login,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    //ClearPassword = u.ClearPassword,
                    Email = u.Email,
                    typeID = u.TypeId,
                    typeName = u.Type.Name,
                    Icon = u.Icon,
                    company = _company,
                    phone = _phone,
                    mailing_state = _mailing_state,
                    StatusID = u.StatusId,
                    StatusName = u.Status.Name,
                    EnrollmentDate = u.CreatedDate,
                    roles = new List<RoleViewModel>(),
                    packages = new List<PackageViewModel>()
                };

                foreach (var rid in u.UserRoleUser.Select(p => p.RoleId).ToList())
                    account.roles.Add(RoleDictionary.First(x => x.id == rid));

                foreach (var cs in u.Subscription.ToList())
                {
                    var rp = RatePlanDictionary.First(p => p.Id == cs.PackageRatePlanId);
                    var item = new PackageViewModel()
                    {
                        ID = rp.PackageId,
                        Name = rp.Package.Name,
                        BoughtDate = cs.EffectiveDate,
                        term = cs.ExpirationDate,
                        NextBillingDate = cs.ExpirationDate,
                        NumberOfMonths = rp.DurationInMonths,
                        Price = rp.Price,
                        Status = cs.StatusId == 12 ? "Active" : "Inactive",
                        Services = new List<ServiceViewModel>()
                    };

                    foreach (var ps in PackageServiceDictionary.Where(p => p.PackageId == rp.PackageId).ToList())
                        item.Services.Add(ServiceDictionary.First(s => s.ID == ps.ServiceId));

                    account.packages.Add(item);
                }
                result.Add(account);
            }

			return result;
		}

        public void AssignFunction(int FunctionID, int RoleID, string access_token)
		{
            VerifyFunction(SecurityFunctions.LandingPage_RO, access_token);

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
            VerifyFunction(SecurityFunctions.LandingPage_RO, access_token);

            var rf = DbContext.RoleFunction.Where(p => p.RoleId == RoleID && p.FunctionId == FunctionID).FirstOrDefault();
            if (rf != null)
            {
                DbContext.RoleFunction.Remove(rf);
                DbContext.SaveChanges();
            }
        }

        public void UpdateCode(int yyyy, int mm, string Code, string access_token)
        {
            IsAdmin(access_token);

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
            VerifyFunction(SecurityFunctions.PromoEQP_CrUD, access_token);

            return DbContext.PromoCode.OrderBy(y => y.Yyyy).OrderBy(m => m.Mm).ToList();
        }

        public void DeletePromoCode(int yyyy, int mm, string access_token)
        {
            IsAdmin(access_token);

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
            IsAdmin(access_token);

            foreach (var c in codes)
                UpdateCode(c.Yyyy, c.Mm, c.Code, access_token);
        }

        public List<PromoUserViewModel> GetPromoSuppliers(string access_token)
        {
            VerifyFunction(SecurityFunctions.PromoEQP_RO, access_token);
          
            var userId = GetUserID(access_token);
            List<PromoUserViewModel> result = new List<PromoUserViewModel>();
            foreach (var u in DbContext.PromoSupplier.Include("PromoProduct").Include("FileLibrary").OrderBy(p => p.Id).ToList())
            {
                var logoPath = string.Empty;
                var logoName = string.Empty;
                if (u.FileLibrary != null){
                    logoPath = _fileManager.GetFilePath(StorageType.SupplierAssets, u.FileLibrary, userId);
                    logoName = u.FileLibrary.FileName; 
                }
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
                    customCode = u.CustomCode,
                    notes = u.notes,
                    externalLink = u.externalLink,
                    DocumentPath = u.DocumentPath,
                    logoPath = logoPath,
                    logoName = logoName
                };
                var lp = u.PromoProduct.Select(p => p.Name);
                if (lp != null)
                    item.products = lp.ToArray();

                result.Add(item);
            }
            return result.OrderBy(x => x.name).ToList();
        }

        public void UpdatePromoUser(PromoUserViewModel u, string access_token)
        {
            VerifyFunction(SecurityFunctions.ePages_RO, access_token);

            bool isNew = false;
            int? logoId = null;
            int userId = _pagesService.GetUserID(access_token);
            var item = DbContext.PromoSupplier.Where(p => p.Id == u.id).FirstOrDefault();
            if (item == default(PromoSupplier))
            {
                item = new PromoSupplier();
                isNew = true;
            }

            bool alreadyHasFile = !string.IsNullOrEmpty(u.logoName);
            bool newFileUploaded =  !string.IsNullOrEmpty(u.upload_file?.FileName);

            if(newFileUploaded)
            {
                logoId = _fileManager.SaveFile(StorageType.SupplierAssets, u.upload_file, userId);
            }
            else if(!alreadyHasFile)
            {
                item.LogoID = null;
                _fileManager.RemoveFile(StorageType.SupplierAssets, userId, item.LogoID.GetValueOrDefault());
            }

            // if (string.IsNullOrEmpty(u.logoName) && !string.IsNullOrEmpty(u.upload_file?.FileName))
            // {
            //     logoId = _fileManager.SaveFile(StorageType.SupplierAssets, u.upload_file, userId);
            // }
            /*else if (!string.IsNullOrEmpty(u.logoName) && !string.IsNullOrEmpty(u.upload_file?.FileName))
            {
                _fileManager.RemoveFile(StorageType.SupplierAssets, userId, item.LogoID.GetValueOrDefault());
                logoId = _fileManager.SaveFile(StorageType.SupplierAssets, u.upload_file, userId);
            }
            else if (u.upload_file == null)
            {
                _fileManager.RemoveFile(StorageType.SupplierAssets, userId, item.LogoID.GetValueOrDefault());
            }*/

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
            item.notes = u.notes;
            item.externalLink = u.externalLink;
            item.DocumentPath = u.DocumentPath;
            if(logoId != null)
            {
                item.LogoID = logoId;
            }

            if (isNew)
                DbContext.Set<PromoSupplier>().Add(item);

            if (!isNew)
                foreach (var pp in DbContext.PromoProduct.Where(p => p.SupplierId == u.id).ToList())
                    DbContext.Set<PromoProduct>().Remove(pp);

            if (u.products != null)
                foreach (var pp in u.products)
                {
                    PromoProduct npp = new PromoProduct()
                    {
                        SupplierId = u.id,
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
            VerifyFunction(SecurityFunctions.LandingPage_RO, access_token);

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
            VerifyFunction(SecurityFunctions.LandingPage_RO, access_token);

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
            VerifyFunction(SecurityFunctions.LandingPage_RO, access_token);

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
            VerifyFunction(SecurityFunctions.LandingPage_RO, access_token);

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
            VerifyFunction(SecurityFunctions.LandingPage_RO, access_token);

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

        public void VerifyFunction(SecurityFunctions functionName, string access_token)
        {
            var functionId = (int)functionName;
            var fs = GetFunctionsForUser(access_token);
            if (!fs.Contains(functionId)){
                throw new Exception("Access denied");
            }   
        }

        public void DeletePromoUser(int ID, string token)
        {
            IsAdmin(token);

            foreach (var pp in DbContext.PromoProduct.Where(p => p.SupplierId == ID).ToList())
                DbContext.Set<PromoProduct>().Remove(pp);

            var item = DbContext.PromoSupplier.Where(p => p.Id == ID).FirstOrDefault();
            if (item != default(PromoSupplier))
            {
                DbContext.Set<PromoSupplier>().Remove(item);
                DbContext.SaveChanges();
            }
        }

        public void UpdateUser(AccountViewModel u, string token)
        {
            VerifyFunction(SecurityFunctions.LandingPage_RO, token);

            var item = DbContext.Users.Where(p => p.Id == u.id).FirstOrDefault();
            if (item != default(Users))
            {
                item.FirstName = u.FirstName;
                item.LastName = u.LastName;
                //item.ClearPassword = u.ClearPassword;
                item.Email = u.Email;
                item.Icon = u.Icon;
                DbContext.SaveChanges();
            }
            else
                throw new Exception("User with ID = " + u.id.ToString() + " not found");


            var dist = DevDbContext.distributors.FirstOrDefault(p => p.id == u.id);
            if (dist != null)
            {
                dist.company = u.company;
                dist.phone = u.phone;
                dist.mailing_state = u.mailing_state;
                DevDbContext.SaveChanges();
            }
        }

        public FileStream DownloadSupplierPDF(string token)
        {
            VerifyFunction(SecurityFunctions.PromoEQP_RO, token);

            string SupplierPDFPath = DbContext.PaymentSetting.First().SupplierPDFPath;
            return File.OpenRead(SupplierPDFPath);
        }

        void IsAdmin(string token)
        {
            var u = DbContext.UserToken.Where(p => p.Token == token).FirstOrDefault();
            if (u == default(UserToken))
                throw new Exception("Not authorized");

            var ur = DbContext.Users.First(p => p.Id == u.UserId);

            if (ur.TypeId != 4)
                throw new Exception("Only admin is authorized to perform this action");
        }
    }
}
