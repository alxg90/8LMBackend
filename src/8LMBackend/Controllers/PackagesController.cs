using _8LMBackend.DataAccess.Models;
using _8LMBackend.DataAccess.DtoModels;
using System.Collections.Generic;
using _8LMBackend.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;
using System;
namespace _8LMCore.Controllers
{
    public class PackagesController : Controller
    {        
        private readonly ISubscribeService _subscribeService;

        public PackagesController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }
        [HttpPost]
        public JsonResult SavePackage([FromBody]PackageDto package, string token){
            if(!_subscribeService.CheckPackageNameValid(package.Name)){
                return Json(new{status = "fail", message = "Package name is already exist."});
            }
            if(package.Id == 0){
                var user = _subscribeService.GetUserByToken(token);
                Package pack = new Package();

                pack.Name = package.Name;
                pack.CreatedBy = user.Id;
                pack.CreatedDate = DateTime.Now;
                pack.CurrencyId = package.Currency;
                pack.Description = "empty";
                pack.DurationInMonth = package.Duration;                
                pack.IsActual = null;
                pack.PaletteId = package.PaletteId;
                pack.Price = package.Price;
                pack.UserTypeId = user.TypeId; 

                _subscribeService.SavePackage(pack);

                foreach(ServicesDto serviceId in package.Services){
                    pack.PackageService.Add(new PackageService(){
                        PackageId = pack.Id,
                        ServiceId = serviceId.Id
                    });
                }   
                foreach(PackageReferenceCodeDto refCode in package.PackageReferenceCode){
                    pack.PackageReferenceCode.Add(new PackageReferenceCode(){
                        PackageId = refCode.PackageId,
                        ReferenceCode = refCode.ReferenceCode,
                        IsFixed = refCode.IsFixed,
                        Value = refCode.Value
                    });
                }
                foreach(PackageReferenceExtendCodeDto refExtendCode in package.PackageReferenceExtendCode){
                    pack.PackageReferenceExtendCode.Add(new PackageReferenceExtendCode(){
                        PackageId = refExtendCode.PackageId,
                        ReferenceCode = refExtendCode.ReferenceCode,
                        Months = refExtendCode.Months
                    });
                }
                foreach(PackageReferenceServiceCodeDto refServiceCode in package.PackageReferenceServiceCode){
                    pack.PackageReferenceServiceCode.Add(new PackageReferenceServiceCode(){
                        PackageId = refServiceCode.PackageId,
                        ReferenceCode = refServiceCode.ReferenceCode,
                        ServiceId = refServiceCode.ServiceId
                    });
                }
                UpdatePackage(pack);

                return Json(new{status = "ok", id = pack.Id});
            }
            else
            {
                return Json(new{status = "fail", message = "Package is actual, so not saved"});
            }
        }
        public JsonResult DeletePackage(int id){
            try
            {
                _subscribeService.DeletePackage(id);
                return Json(new{status = "ok", message = "Package was deleted"});
            }
            catch(System.Exception ex)
            {
                return Json(new{status = "fail", message = ex.Message});
            }
        }
        public JsonResult UpdatePackage(Package package){            
            try
            {
                _subscribeService.UpdatePackage(package);
                return Json(new{status = "ok", message = "Package was updated"});
            }
            catch (System.Exception ex)
            {                
                return Json(new{status = "fail", message = ex.Message});
            }
        }
        public Service[] GetAllServices(){
            try
            {
               return _subscribeService.GetAllServices();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
        public JsonResult PrepareInvoice(int PackageID, string token, string ReferenceCode = null){
            var invoice = _subscribeService.PrepareInvoice(PackageID, token, ReferenceCode);
            return Json(new {InvoiceId = invoice.Id, Field1 = "", HashCode = ""});
        }
        public JsonResult SetActive(int id, int setActual){
            try
            {
                _subscribeService.SetActive(id, setActual);
                return Json(new{status = "ok", message = "Package isActual was seted to" + setActual});
            }
            catch(Exception ex)
            {
                return Json(new{status = "fail", message = ex.Message});
            }
        }
        public JsonResult AcceptPayment(RelayAuthorizeNetresponse rel){
            try
            {
                _subscribeService.AcceptPayment(rel);
                return Json(new{status = "ok", message = "Payment was done"});
            }
            catch(Exception ex)
            {
                return Json(new{status = "fail", message = ex.Message});
            }            
        }
        public Package GetPackageById(int id){
            try
            {
                return _subscribeService.GetPackageById(id);
            }
            catch (System.Exception ex)
            {                
                throw new Exception(ex.Message);
            }            
        }
        public List<PackageDto> GetAllPackages(){
            try
            {
                var packages = _subscribeService.GetAllPackages();
                var services = GetAllServices();
                var packageDto = new List<PackageDto>();
                foreach (var item in packages)
                {
                    var tempServ = services.Where(x=>x.Id == item.Id);
                    PackageDto pack = new PackageDto();
                    pack.Id = item.Id;
                    pack.Name = item.Name;
                    var servDto = new List<ServicesDto>();
                    foreach (var s in tempServ)
                    {
                        servDto.Add(new ServicesDto(){
                            Id = s.Id,
                            Name = s.Name
                        });
                    }
                    pack.Services = servDto.ToArray();
                    pack.PaletteId = item.PaletteId;
                    pack.Duration = item.DurationInMonth;
                    pack.Price = item.Price;
                    pack.Currency = item.CurrencyId;
                    var tempRefCode = new List<PackageReferenceCodeDto>();
                    foreach (var packageReferenceCode in item.PackageReferenceCode)
                    {
                        tempRefCode.Add(new PackageReferenceCodeDto(){
                            PackageId = packageReferenceCode.PackageId,
                            ReferenceCode = packageReferenceCode.ReferenceCode,
                            IsFixed = packageReferenceCode.IsFixed,
                            Value = packageReferenceCode.Value
                        });
                    }
                    pack.PackageReferenceCode = tempRefCode.ToArray();
                    var tempRefServCode = new List<PackageReferenceServiceCodeDto>();
                    foreach (var packageReferenceServiceCode in item.PackageReferenceServiceCode)
                    {
                        tempRefServCode.Add(new PackageReferenceServiceCodeDto(){
                            PackageId = packageReferenceServiceCode.PackageId,
                            ReferenceCode = packageReferenceServiceCode.ReferenceCode,
                            ServiceId = packageReferenceServiceCode.ServiceId
                        });
                    }
                    pack.PackageReferenceServiceCode = tempRefServCode.ToArray();
                    var tempRefExtCode = new List<PackageReferenceExtendCodeDto>();
                    foreach (var packageReferenceExtendCode in item.PackageReferenceExtendCode)
                    {
                        tempRefExtCode.Add(new PackageReferenceExtendCodeDto(){
                            PackageId = packageReferenceExtendCode.PackageId,
                            ReferenceCode = packageReferenceExtendCode.ReferenceCode,
                            Months = packageReferenceExtendCode.Months
                        });
                    }
                    pack.PackageReferenceExtendCode = tempRefExtCode.ToArray();
                    packageDto.Add(pack);
                }
                return packageDto;
            }
            catch (System.Exception ex)
            {                
                throw new Exception(ex.Message);
            }            
        }
    }
}
