using _8LMBackend.DataAccess.Models;
using _8LMBackend.DataAccess.DtoModels;
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

                foreach(int serviceId in package.Services){
                    pack.PackageService.Add(new PackageService(){
                        PackageId = pack.Id,
                        ServiceId = serviceId
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
            return _subscribeService.GetAllServices();
        }
        public JsonResult PrepareInvoice(int PackageID, string token, string ReferenceCode = null){
            var invoice = _subscribeService.PrepareInvoice(PackageID, token, ReferenceCode);
            return Json(new {InvoiceId = invoice.Id, Field1 = "", HashCode = ""});
        }
        public JsonResult SetActive(int id, int isActual){
            try
            {
                _subscribeService.SetActive(id, isActual);
                return Json(new{status = "ok", message = "Package isActual was seted to" + isActual});
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
        public Package[] GetAllPackages(){
            try
            {
                return _subscribeService.GetAllPackages();
            }
            catch (System.Exception ex)
            {                
                throw new Exception(ex.Message);
            }            
        }
    }
}
