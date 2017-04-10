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
            if(!_subscribeService.CheckPackageNameValid(package.Name, package.Id)){
                return Json(new{status = "fail", message = "Package name is already exist."});
            }
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

                if(package.Id == 0)
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
            var package = _subscribeService.GetPackageById(PackageID);
            var customHash = new CustomHash();

            return Json(new {InvoiceId = invoice.Id, invoice.AmountDue, HashCode = customHash.GetHashedString(((decimal)invoice.AmountDue / 100).ToString(), invoice.Id.ToString()), TimeStamp = customHash.ConvertToUnixTimestamp(DateTime.UtcNow).ToString()});
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
        public ActionResult AcceptPayment(RelayAuthorizeNetresponseDto relDto){
            try
            {
                RelayAuthorizeNetresponse rel = new RelayAuthorizeNetresponse();
                rel.CreatedDate = DateTime.UtcNow;
                rel.InvoiceId = Convert.ToInt32(relDto.x_invoice_num);
                rel.XAccountNumber = relDto.x_account_number;
                rel.XAddress = relDto.x_address;
                rel.XAmount = relDto.x_amount;
                rel.XAuthCode = relDto.x_auth_code;
                rel.XAvsCode = relDto.x_avs_code;
                rel.XCardType = relDto.x_card_type;
                rel.XCavvResponse = relDto.x_cavv_response;
                rel.XCity = relDto.x_city;
                rel.XCompany = relDto.x_company;
                rel.XCountry = relDto.x_country;
                rel.XCustId = relDto.x_cust_id;
                rel.XCvv2RespCode = relDto.x_cvv2_resp_code;
                rel.XDescription = relDto.x_description;
                rel.XDuty = relDto.x_duty;
                rel.XEmail = relDto.x_email;
                rel.XFax = relDto.x_fax;
                rel.XFirstName = relDto.x_first_Name;
                rel.XFreight = relDto.x_freight;
                rel.XInvoiceNum = relDto.x_invoice_num;
                rel.XLastName = relDto.x_last_name;
                rel.XMd5Hash = relDto.x_MD5_Hash;
                rel.XMethod = relDto.x_method;
                rel.XPhone = relDto.x_phone;
                rel.XPoNum = relDto.x_po_num;
                rel.XResponseCode = relDto.x_response_code;
                rel.XResponseReasonCode = relDto.x_response_reason_code;
                rel.XResponseReasonText = relDto.x_response_reason_text;
                rel.XSha2Hash = relDto.x_SHA2_Hash;
                rel.XShipToAddress = relDto.x_ship_to_address;
                rel.XShipToCity = relDto.x_ship_to_city;
                rel.XShipToCompany = relDto.x_ship_to_company;
                rel.XShipToCountry = relDto.x_ship_to_country;
                rel.XShipToFirstName = relDto.x_ship_to_first_name;
                rel.XShipToLastName = relDto.x_ship_to_last_name;
                rel.XShipToState = relDto.x_ship_to_state;
                rel.XShipToZip  = relDto.x_ship_to_zip;
                rel.XState  = relDto.x_state;
                rel.XTax = relDto.x_tax;
                rel.XTaxExempt = relDto.x_tax_exempt;
                rel.XTestRequest = relDto.x_test_request;
                rel.XTransId = relDto.x_trans_id;
                rel.XType = relDto.x_type;
                rel.XZip = relDto.x_zip;

                _subscribeService.AcceptPayment(rel);
                return View();
            }
            catch(Exception ex)
            {
                return Json(new{status = "fail", message = ex.Message});
            }            
        }
        public PackageDto GetPackageById(int id){
            try
            {
                return ToDtoPackage(_subscribeService.GetPackageById(id), _subscribeService.GetAllServices(), false);
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
                    packageDto.Add(ToDtoPackage(item, services, false));
                }
                return packageDto;
            }
            catch (System.Exception ex)
            {                
                throw new Exception(ex.Message);
            }            
        }
        public List<PackageDto> GetUserPackages(string token){
            var user = _subscribeService.GetUserByToken(token);
            var packages = _subscribeService.GetAllPackages();
            var packageDto = new List<PackageDto>();            
            var services = GetAllServices();
            foreach (var item in packages)
            {      
                var pack = ToDtoPackage(item, services, true);   
                var subscription = _subscribeService.GetSubscriptionForPackage(item.Id, user.Id);     
                if(item.IsActual == 1){          
                    if(subscription != null){
                        pack.ValidTo = subscription.ExpirationDate;
                    }                
                    packageDto.Add(pack);  
                } else {
                    if(subscription != null){
                        pack.ValidTo = subscription.ExpirationDate;
                        packageDto.Add(pack);
                    }                
                }
            }
            
            return packageDto;
        }
        private PackageDto ToDtoPackage(Package item, Service[] services, bool isUser){
            var dbPackageReferenceCodes = _subscribeService.GetPackageReferenceCodeById(item.Id);
            var dbPackageReferenceExtendCodes = _subscribeService.GetPackageReferenceExtendCodeById(item.Id);
            var dbPackageReferenceServiceCodes = _subscribeService.GetPackageReferenceServiceCodeById(item.Id);
            var packageServices = _subscribeService.GetPackageServicesById(item.Id);
            PackageDto pack = new PackageDto();
            pack.Id = item.Id;
            pack.Name = item.Name;
            var servDto = new List<ServicesDto>();
            foreach (var s in packageServices)
            {
                servDto.Add(new ServicesDto(){
                    Id = s.ServiceId,
                    Name = services.FirstOrDefault(x => x.Id == s.ServiceId).Name
                });
            }
            pack.Services = servDto.ToArray();
            pack.PaletteId = item.PaletteId;
            pack.Duration = item.DurationInMonth;
            pack.Price = item.Price;
            pack.Currency = item.CurrencyId;
            pack.IsActual = item.IsActual;

            if(!isUser){
                var tempRefCode = new List<PackageReferenceCodeDto>();
                foreach (var packageReferenceCode in dbPackageReferenceCodes)
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
                        ServiceId = packageReferenceServiceCode.ServiceId,
                        ServiceName = services.FirstOrDefault(x => x.Id == packageReferenceServiceCode.ServiceId).Name
                    });
                }
                pack.PackageReferenceServiceCode = tempRefServCode.ToArray();
                var tempRefExtCode = new List<PackageReferenceExtendCodeDto>();
                foreach (PackageReferenceExtendCode packageReferenceExtendCode in dbPackageReferenceExtendCodes)
                {
                    tempRefExtCode.Add(new PackageReferenceExtendCodeDto(){
                        PackageId = packageReferenceExtendCode.PackageId,
                        ReferenceCode = packageReferenceExtendCode.ReferenceCode,
                        Months = packageReferenceExtendCode.Months
                    });
                }
                pack.PackageReferenceExtendCode = tempRefExtCode.ToArray();
            }
            return pack;
        }
    }
}
