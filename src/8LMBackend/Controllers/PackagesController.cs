using _8LMBackend.DataAccess.Models;
using _8LMBackend.DataAccess.DtoModels;
using System.Collections.Generic;
using _8LMBackend.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net;
using System.Text;

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
                _8LMBackend.Logger.SaveLog("Error: Package name is already exist.");
                return Json(new{status = "fail", message = "Package name is already exist."});
            }
                var user = _subscribeService.GetUserByToken(token);
                Package pack = new Package();

                pack.Name = package.Name;
                pack.CreatedBy = user.Id;
                pack.CreatedDate = DateTime.Now;
                //pack.CurrencyId = package.Currency;
                pack.Description = "empty";
                //pack.DurationInMonth = package.Duration;                
                pack.StatusId = Statuses.Package.New;
                pack.PaletteId = package.PaletteId;
                //pack.Price = package.Price;
                pack.UserTypeId = user.TypeId; 

                if(package.Id == 0)
                    _subscribeService.SavePackage(pack);

                var packRate = new PackageRatePlan();
                packRate.DurationInMonths = package.Duration;
                packRate.Price = package.Price;
                packRate.PackageId = pack.Id;

                foreach(ServicesDto serviceId in package.Services){
                    pack.PackageService.Add(new PackageService(){
                        PackageId = pack.Id,
                        ServiceId = serviceId.Id
                    });
                }   
                foreach(PackageReferenceCodeDto refCode in package.PackageReferenceCode){
                    packRate.PackageReferenceCode.Add(new PackageReferenceCode(){
                        PackageRatePlanId = packRate.Id,
                        ReferenceCode = refCode.ReferenceCode,
                        IsFixed = refCode.IsFixed,
                        Value = refCode.Value
                    });
                }
                foreach(PackageReferenceExtendCodeDto refExtendCode in package.PackageReferenceExtendCode){
                    packRate.PackageReferenceExtendCode.Add(new PackageReferenceExtendCode(){
                        PackageRatePlanId = packRate.Id,
                        ReferenceCode = refExtendCode.ReferenceCode,
                        Months = refExtendCode.Months
                    });
                }
                foreach(PackageReferenceServiceCodeDto refServiceCode in package.PackageReferenceServiceCode){
                    packRate.PackageReferenceServiceCode.Add(new PackageReferenceServiceCode(){
                        PackageRatePlanId = packRate.Id,
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
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
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
                _8LMBackend.Logger.SaveLog(ex.StackTrace);  
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
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                throw new Exception(ex.Message);
            }            
        }
        public JsonResult PrepareInvoice(int PackageID, string token, string ReferenceCode = null){
            try{
                var invoice = _subscribeService.PrepareInvoice(PackageID, token, ReferenceCode);
                var package = _subscribeService.GetPackageById(PackageID);
                var customHash = new CustomHash();

                return Json(new {InvoiceId = invoice.Id, invoice.AmountDue, HashCode = customHash.GetHashedString(((decimal)invoice.AmountDue / 100).ToString(), invoice.Id.ToString()), TimeStamp = customHash.ConvertToUnixTimestamp(DateTime.UtcNow).ToString()});
            }
            catch(Exception ex)
            {
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                throw ex;
            }
        }
        public JsonResult PrepareGuestInvoice(int PackageID, string ReferenceCode = null){
            try{
                var invoice = _subscribeService.PrepareInvoice(PackageID, null, ReferenceCode);
                var package = _subscribeService.GetPackageById(PackageID);
                var customHash = new CustomHash();

                return Json(new {InvoiceId = invoice.Id, invoice.AmountDue, HashCode = customHash.GetHashedString(((decimal)invoice.AmountDue / 100).ToString(), invoice.Id.ToString()), TimeStamp = customHash.ConvertToUnixTimestamp(DateTime.UtcNow).ToString()});
            }
            catch(Exception ex)
            {
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                throw ex;
            }
        }
        public JsonResult SetActive(int id, int setActual){
            try
            {
                _subscribeService.SetActive(id, setActual);
                return Json(new{status = "ok", message = "Package StatusId was seted to" + setActual});
            }
            catch(Exception ex)
            {
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                return Json(new{status = "fail", message = ex.Message});
            }
        }
        public ActionResult AcceptPayment(RelayAuthorizeNetresponseDto relDto){
            try
            {
                if(relDto.x_response_code != 1){
                    _8LMBackend.Logger.SaveLog(relDto.x_response_reason_text);
                    return Json(new{status = "fail", message = relDto.x_response_reason_text});
                }
                var rel = RelayDtoToNormal(relDto);
                _subscribeService.AcceptPayment(rel);
                var userToken = _subscribeService.GetTokenByInvoice(rel.Invoice);
                ViewBag.userToken = userToken;
                
                return View();
            }
            catch(Exception ex)
            {
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                return Json(new{status = "fail", message = ex.Message});
            }            
        }
        public ActionResult AcceptGuestPayment(RelayAuthorizeNetresponseDto relDto){
                if(relDto.x_response_code != 1){
                    _8LMBackend.Logger.SaveLog(relDto.x_response_reason_text);
                    return Json(new{status = "fail", message = relDto.x_response_reason_text});
                }               
                var invoice = _subscribeService.AcceptGuestPayment(RelayDtoToNormal(relDto));
                CreateCustomerProfileFromTransaction(invoice.UserId, relDto.x_trans_id);
                return View();          
        }
        public PackageDto GetPackageById(int id){
            try
            {
                return ToDtoPackage(_subscribeService.GetPackageById(id), _subscribeService.GetPackageRatePlanByPackageID(id), _subscribeService.GetAllServices(), false);
            }
            catch (System.Exception ex)
            {                
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                throw new Exception(ex.Message);
            }            
        }
        public List<PackageDto> GetAllPackages(string token){
            try
            {
                var user = _subscribeService.GetUserByToken(token);
                if(user!=null){
                    var packages = _subscribeService.GetAllPackages();
                    var services = GetAllServices();
                    var packageDto = new List<PackageDto>();
                    foreach (var item in packages)
                    {
                        packageDto.Add(ToDtoPackage(item, _subscribeService.GetPackageRatePlanByPackageID(item.Id), services, false));
                    }
                    return packageDto;
                } else {
                    throw new Exception("Invalid user");
                }
            }
            catch (System.Exception ex)
            {     
                _8LMBackend.Logger.SaveLog(ex.StackTrace);           
                throw new Exception(ex.Message);
            }            
        }
        public List<PackageDto> GetActivePackages(){
            try
            {
                var packages = _subscribeService.GetActivePackages();
                var services = GetAllServices();
                var packageDto = new List<PackageDto>();        
                foreach (var item in packages)
                {      
                    var pack = ToDtoPackage(item, _subscribeService.GetPackageRatePlanByPackageID(item.Id), services, true);
                    packageDto.Add(pack);                  
                }
            return packageDto;
            }
            catch (System.Exception ex)
            {
                _8LMBackend.Logger.SaveLog(ex.StackTrace);  
                throw new Exception(ex.Message);
            }
        }
        public List<PackageDto> GetUserPackages(string token){
            try{
            var user = _subscribeService.GetUserByToken(token);
            var packages = _subscribeService.GetAllPackages();
            var packageDto = new List<PackageDto>();            
            var services = GetAllServices();
            foreach (var item in packages)
            {      
                var pack = ToDtoPackage(item, _subscribeService.GetPackageRatePlanByPackageID(item.Id), services, true);   
                var subscription = _subscribeService.GetSubscriptionForPackage(item.Id, user.Id);     
                if(item.StatusId == Statuses.Package.New){          
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
            catch(Exception ex)
            {
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                throw ex;
            }
        }
        private PackageDto ToDtoPackage(Package item, PackageRatePlan ratePlan, Service[] services, bool isUser){
            try{    
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
                pack.Duration = ratePlan.DurationInMonths;
                pack.Price = ratePlan.Price;
                //pack.Currency = item.CurrencyId;
                pack.StatusId = item.StatusId;

                if(!isUser){
                    var tempRefCode = new List<PackageReferenceCodeDto>();
                    foreach (var packageReferenceCode in dbPackageReferenceCodes)
                    {
                        tempRefCode.Add(new PackageReferenceCodeDto(){
                            PackageId = ratePlan.PackageId,
                            ReferenceCode = packageReferenceCode.ReferenceCode,
                            IsFixed = packageReferenceCode.IsFixed,
                            Value = packageReferenceCode.Value
                        });
                    }
                    pack.PackageReferenceCode = tempRefCode.ToArray();
                    var tempRefServCode = new List<PackageReferenceServiceCodeDto>();
                    foreach (var packageReferenceServiceCode in ratePlan.PackageReferenceServiceCode)
                    {
                        tempRefServCode.Add(new PackageReferenceServiceCodeDto(){
                            PackageId = ratePlan.PackageId,
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
                            PackageId = ratePlan.PackageId,
                            ReferenceCode = packageReferenceExtendCode.ReferenceCode,
                            Months = packageReferenceExtendCode.Months
                        });
                    }
                    pack.PackageReferenceExtendCode = tempRefExtCode.ToArray();
                }
                return pack;
            }
            catch(Exception ex)
            {
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                throw ex;
            }
        }
        private RelayAuthorizeNetresponse RelayDtoToNormal(RelayAuthorizeNetresponseDto relDto){
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
                return rel;
        }
        public async void CreateCustomerProfileFromTransaction(int userId, long transactionID)
        {
            await _subscribeService.SaveCustomerProfile(userId, transactionID);
        }
        public void CreateTransaction(int invoiceId)
         {
        // 1. GET customerProfileId, customerPaymentProfileId, Amount based on Invoice
            var authProfile = _subscribeService.GetAuthProfileByInvoice(invoiceId);
        // 2. SEND POST REQUEST TO createTransactionRequest
            _subscribeService.СreateTransactionRequest(authProfile.CustomerProfileId, authProfile.PaymentProfileId, invoiceId);
        // 3. Insert record into AuthorizeNETTransaction table

        // 4. Update Invoice table
        }

        public void CaptureTransaction(int invoiceId)
        {
            try{
            var authProfile = _subscribeService.GetAuthProfileByInvoice(invoiceId);
        // 1. GET TransactionID and Amount based on Invoice //Check that transaction is not captured
        _subscribeService.СaptureTransactionRequest(authProfile.CustomerProfileId, authProfile.PaymentProfileId, invoiceId);
            }
            catch
            (Exception ex){
                throw ex;
            }
        // 2. SEND POST REQUEST TO createTransactionRequest
        // 3. Update AuthorizeNETTransaction table
        // 4. Update Invoice table

        }
    }
}
