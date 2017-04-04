using System;
using System.Collections.Generic;
using System.Linq;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.Service
{
    public class SubscribeService : ServiceBase, ISubscribeService
    {
		public SubscribeService(IDbFactory dbFactory)
			: base(dbFactory) 
		{
		}
        public void SavePackage(Package package){
            DbContext.Package.Add(package);
            DbContext.SaveChanges();
        }
        public void DeletePackage(int id){
            var package = DbContext.Package.FirstOrDefault(x => x.Id == id);
            if(package.IsActual==null){
                DbContext.Package.Remove(package);
                DbContext.SaveChanges();
            }
        }
        public void UpdatePackage(Package package){
            DbContext.Package.Update(package);
            DbContext.SaveChanges();
        }

        public _8LMBackend.DataAccess.Models.Service[] GetAllServices(){
            var services = DbContext.Service.ToArray();
            return (services.Length > 0) ? services : new _8LMBackend.DataAccess.Models.Service[0];
        } 
        public Invoice PrepareInvoice(int PackageID, string token, string ReferenceCode = null){
            var user = DbContext.UserToken.Where(p => p.Token == token).FirstOrDefault();
            if (user == default(UserToken))
                throw new Exception("Not authorized");

            var invoice = new Invoice();
            var package = DbContext.Package.FirstOrDefault(x => x.Id == PackageID);
            invoice.UserId = user.Id;
            invoice.Amount = package.Price;
            
            var tempDiscount = DbContext.PackageReferenceCode.FirstOrDefault(x => x.PackageId == PackageID);
            invoice.Discount = tempDiscount.IsFixed ? tempDiscount.Value : invoice.Amount * tempDiscount.Value / 100;
            
            invoice.AmountDue = invoice.Amount - invoice.Discount ;
            invoice.StatusId = null;
            invoice.CreatedDate = DateTime.UtcNow;
            invoice.UpdatedDate = null;
            return invoice;
        }
        public void AcceptPayment(RelayAuthorizeNetresponse rel){
            DbContext.RelayAuthorizeNetresponse.Add(rel);
            DbContext.SaveChanges();
        }

        public Users GetUserByToken(string token){
            var userToken = DbContext.UserToken.Where(p => p.Token == token).FirstOrDefault();
            var user = DbContext.Users.FirstOrDefault(x => x.Id == userToken.UserId);
            return user;
        }

        public Package GetPackageById(int id){
            return DbContext.Package.FirstOrDefault(x => x.Id == id);
        }
        public Package[] GetAllPackages(){
            Package[] packages = DbContext.Package.ToArray();
            return packages;
        }
        public List<PackageService> GetPackageServicesById(int packageId){
            return DbContext.PackageService.Where(x => x.PackageId == packageId).ToList();
        }

        public bool CheckPackageNameValid(string name, int? packageId){
            var pack = DbContext.Package.FirstOrDefault(x => x.Name == name);
            if(packageId==0)
            {
                return pack == null;
            }
            else
            {
                if(pack == null || pack.Id == packageId){
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void SetActive(int id, int setActual){
            var package = DbContext.Package.FirstOrDefault(x => x.Id == id);
            package.IsActual = setActual;
            DbContext.Package.Update(package);
            DbContext.SaveChanges();
        }
        public List<PackageReferenceCode> GetPackageReferenceCodeById(int packageId){
            return DbContext.PackageReferenceCode.Where(x=>x.PackageId == packageId).ToList();
        }
        public List<PackageReferenceExtendCode> GetPackageReferenceExtendCodeById(int packageId){
            return DbContext.PackageReferenceExtendCode.Where(x=>x.PackageId == packageId).ToList();
        }  
        public List<PackageReferenceServiceCode> GetPackageReferenceServiceCodeById(int packageId){
            return DbContext.PackageReferenceServiceCode.Where(x=>x.PackageId == packageId).ToList();
        }          
    }
}
