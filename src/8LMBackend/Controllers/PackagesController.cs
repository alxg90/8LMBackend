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
        public JsonResult SavePackage([FromBody]PackageDto package, string token)
        {
            if(!_subscribeService.CheckPackageNameValid(package.Name, package.Id))
            {
                _8LMBackend.Logger.SaveLog("Error: Package name is already exist.");
                return Json(new{status = "fail", message = "Package name is already exist."});
            }
                var user = _subscribeService.GetUserByToken(token);
                Package pack = new Package();

                pack.Name = package.Name;
                pack.CreatedBy = user.Id;
                pack.CreatedDate = DateTime.Now;
                pack.Description = "empty";             
                pack.StatusId = Statuses.Package.New;
                pack.PaletteId = package.PaletteId;
                pack.UserTypeId = user.TypeId; 

                if(package.Id == 0)
                    _subscribeService.SavePackage(pack);

                foreach(ServicesDto serviceId in package.Services)
                {
                    pack.PackageService.Add(new PackageService()
                    {
                        PackageId = pack.Id,
                        ServiceId = serviceId.Id
                    });
                }
                foreach(var packRateDto in package.PackageRatePlans)
                {
                    var packRate = new PackageRatePlan();
                    packRate.DurationInMonths = packRateDto.DurationInMonths;
                    packRate.Price = packRateDto.Price;
                    packRate.PackageId = packRateDto.PackageId;

                    foreach(PackageReferenceCodeDto refCode in packRateDto.PackageReferenceCode)
                    {
                        packRate.PackageReferenceCode.Add(new PackageReferenceCode()
                        {
                            PackageRatePlanId = packRate.Id,
                            ReferenceCode = refCode.ReferenceCode,
                            IsFixed = refCode.IsFixed,
                            Value = refCode.Value
                        });
                    }
                    foreach(PackageReferenceExtendCodeDto refExtendCode in packRateDto.PackageReferenceExtendCode)
                    {
                        packRate.PackageReferenceExtendCode.Add(new PackageReferenceExtendCode()
                        {
                            PackageRatePlanId = packRate.Id,
                            ReferenceCode = refExtendCode.ReferenceCode,
                            Months = refExtendCode.Months
                        });
                    }
                    foreach(PackageReferenceServiceCodeDto refServiceCode in packRateDto.PackageReferenceServiceCode)
                    {
                        packRate.PackageReferenceServiceCode.Add(new PackageReferenceServiceCode()
                        {
                            PackageRatePlanId = packRate.Id,
                            ReferenceCode = refServiceCode.ReferenceCode,
                            ServiceId = refServiceCode.ServiceId
                        });
                    }
                    pack.PackageRatePlan.Add(packRate);
                }   
                
                UpdatePackage(pack);

                return Json(new{status = "ok", id = pack.Id});
        }
        public JsonResult DeletePackage(int id)
        {
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
        public JsonResult UpdatePackage(Package package)
        {            
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
        public Service[] GetAllServices()
        {
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
        public JsonResult PrepareInvoice(int PackageRateID, string token, string ReferenceCode = null)
        {
            try
            {
                var userToken = _subscribeService.GetUserToken(token);
                if(userToken==null)
                    return Json(new{ Status = "Fail", Message = "User with that token doesen't exist"});

                var invoice = _subscribeService.PrepareInvoice(PackageRateID, token, ReferenceCode);
                var customHash = new CustomHash();

                return Json(new {InvoiceId = invoice.Id, invoice.AmountDue, HashCode = customHash.GetHashedString(((decimal)invoice.AmountDue / 100).ToString(), invoice.Id.ToString()), TimeStamp = customHash.ConvertToUnixTimestamp(DateTime.UtcNow).ToString()});
            }
            catch(Exception ex)
            {
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                throw ex;
            }
        }
        // public JsonResult PrepareGuestInvoice(int PackageID, string ReferenceCode = null){
        //     try{
        //         var invoice = _subscribeService.PrepareInvoice(PackageID, null, ReferenceCode);
        //         var package = _subscribeService.GetPackageById(PackageID);
        //         var customHash = new CustomHash();

        //         return Json(new {InvoiceId = invoice.Id, invoice.AmountDue, HashCode = customHash.GetHashedString(((decimal)invoice.AmountDue / 100).ToString(), invoice.Id.ToString()), TimeStamp = customHash.ConvertToUnixTimestamp(DateTime.UtcNow).ToString()});
        //     }
        //     catch(Exception ex)
        //     {
        //         _8LMBackend.Logger.SaveLog(ex.StackTrace);
        //         throw ex;
        //     }
        // }
        public JsonResult SetActive(int id, int setActual)
        {
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
        public ActionResult AcceptPayment(RelayAuthorizeNetresponseDto relDto)
        {
            try
            {
                if(relDto.x_response_code != 1)
                {
                    _8LMBackend.Logger.SaveLog(relDto.x_response_reason_text);
                    return Json(new{status = "fail", message = relDto.x_response_reason_text});
                }
                var rel = RelayDtoToNormal(relDto);
                _subscribeService.AcceptPayment(rel);
                var userToken = _subscribeService.GetTokenByInvoice(rel.Invoice);
                ViewBag.userToken = userToken;
                ViewBag.email = rel.XEmail;
                ViewBag.transactionId = rel.XTransId;
                //  После первой транзакции (Accept Payment), 
                // необходимо вызвать миетод, который на осмнове транзакции создаёт пеймент профиль. 
                // Этот профиль записать в таблицу AuthorizeNETCustomerProfile.
                CreateCustomerProfileFromTransaction(_subscribeService.GetUserByToken(userToken).Id, rel.XTransId);
                
                return View();
            }
            catch(Exception ex)
            {
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                return View("PaymentError");
                //return Json(new{status = "fail", message = ex.Message});
            }            
        }
        // public ActionResult AcceptGuestPayment(RelayAuthorizeNetresponseDto relDto)
        // {
        //         if(relDto.x_response_code != 1)
        //         {
        //             _8LMBackend.Logger.SaveLog(relDto.x_response_reason_text);
        //             return Json(new{status = "fail", message = relDto.x_response_reason_text});
        //         }               
        //         var invoice = _subscribeService.AcceptGuestPayment(RelayDtoToNormal(relDto));
        //         CreateCustomerProfileFromTransaction(invoice.UserId, relDto.x_trans_id);
        //         return View();          
        // }
        public PackageDto GetPackageById(int id)
        {
            try
            {
                return ToDtoPackage(_subscribeService.GetPackageById(id), _subscribeService.GetAllServices(), false);
            }
            catch (System.Exception ex)
            {                
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                throw new Exception(ex.Message);
            }            
        }
        public List<PackageDto> GetAllPackages(string token)
        {
            try
            {
                var user = _subscribeService.GetUserByToken(token);
                if(user!=null)
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
                else
                {
                    throw new Exception("Invalid user");
                }
            }
            catch (System.Exception ex)
            {     
                _8LMBackend.Logger.SaveLog(ex.StackTrace);           
                throw new Exception(ex.Message);
            }            
        }
        public List<PackageDto> GetActivePackages()
        {
            try
            {
                var packages = _subscribeService.GetActivePackages();
                var services = GetAllServices();
                var packageDto = new List<PackageDto>();        
                foreach (var item in packages)
                {      
                    var pack = ToDtoPackage(item, services, true);
                    packageDto.Add(pack);                  
                }
                return packageDto.OrderBy(p => p.SortOrder).ToList();
            }
            catch (System.Exception ex)
            {
                _8LMBackend.Logger.SaveLog(ex.StackTrace);  
                throw new Exception(ex.Message);
            }
        }
        public List<PackageDto> GetUserPackages(string token)
        {
            try
            {
                var user = _subscribeService.GetUserByToken(token);
                var packages = _subscribeService.GetAllPackages();
                var packageDto = new List<PackageDto>();            
                var services = GetAllServices();
                foreach (var item in packages)
                {      
                    var pack = ToDtoPackage(item, services, true);   
                    var subscription = _subscribeService.GetSubscriptionForPackage(item.Id, user.Id);     
                    if(item.StatusId == Statuses.Package.Published)
                    {          
                        if(subscription != null)
                        {
                            pack.ValidTo = subscription.ExpirationDate;
                            foreach (var packageRatePlan in pack.PackageRatePlans)
                            {
                                if(packageRatePlan.Id == subscription.PackageRatePlanId && subscription.StatusId == Statuses.Subscription.Active)
                                    packageRatePlan.Bought = true;
                            }
                        }                
                        packageDto.Add(pack);  
                    }
                    else
                    {
                        if(subscription != null)
                        {
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
        private PackageDto ToDtoPackage(Package item, Service[] services, bool isUser)
        {
            try
            {   
                var packageServices = _subscribeService.GetPackageServicesById(item.Id);
                var packageRatePlans = _subscribeService.GetPackageRatePlansByPackageID(item.Id);
                PackageDto pack = new PackageDto();
                pack.PackageRatePlans = new List<PackageRatePlanDto>();
                pack.Id = item.Id;
                pack.Name = item.Name;
                var servDto = new List<ServicesDto>();
                foreach (var s in packageServices)
                {
                    servDto.Add(new ServicesDto()
                    {
                        Id = s.ServiceId,
                        Name = services.FirstOrDefault(x => x.Id == s.ServiceId).Name
                    });
                }
                pack.Services = servDto.ToArray();
                pack.PaletteId = item.PaletteId;
                pack.StatusId = item.StatusId;
                pack.VideoURL = item.VideoURL;
                pack.SortOrder = item.SortOrder;

                foreach(var plan in packageRatePlans)
                {
                    PackageRatePlanDto packRatePlan = RatePlanToDto(plan, services, isUser);
                    pack.PackageRatePlans.Add(packRatePlan);
                }
                return pack;
            }
            catch(Exception ex)
            {
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                throw ex;
            }
        }
        private RelayAuthorizeNetresponse RelayDtoToNormal(RelayAuthorizeNetresponseDto relDto)
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
                return rel;
        }
        public void CreateCustomerProfileFromTransaction(int userId, long transactionID)
        {
            _subscribeService.SaveCustomerProfile(userId, transactionID);
        }
        public void CreateTransaction(int invoiceId)
        {
            var authProfile = _subscribeService.GetAuthProfileByInvoice(invoiceId);
            _subscribeService.СreateTransactionRequest(authProfile.CustomerProfileId, authProfile.PaymentProfileId, invoiceId);
        }

        // public void CaptureTransaction(int invoiceId)
        // {
        //     try
        //     {
        //         var authProfile = _subscribeService.GetAuthProfileByInvoice(invoiceId);
        //         _subscribeService.СaptureTransactionRequest(authProfile.CustomerProfileId, authProfile.PaymentProfileId, invoiceId);
        //     }
        //     catch
        //     (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }
        PackageRatePlanDto RatePlanToDto(PackageRatePlan ratePlan, Service[] services, bool isUser)
        {             
                var dbPackageReferenceCodes = _subscribeService.GetPackageReferenceCodeById(ratePlan.Id);
                var dbPackageReferenceExtendCodes = _subscribeService.GetPackageReferenceExtendCodeById(ratePlan.Id);
                var dbPackageReferenceServiceCodes = _subscribeService.GetPackageReferenceServiceCodeById(ratePlan.Id);
                var tempRefCode = new List<PackageReferenceCodeDto>();
                var tempRefServCode = new List<PackageReferenceServiceCodeDto>();
                var tempRefExtCode = new List<PackageReferenceExtendCodeDto>();
                if(!isUser)
                {                
                    foreach (var packageReferenceCode in dbPackageReferenceCodes)
                    {
                        tempRefCode.Add(new PackageReferenceCodeDto(){
                            PackRateId = ratePlan.Id,
                            ReferenceCode = packageReferenceCode.ReferenceCode,
                            IsFixed = packageReferenceCode.IsFixed,
                            Value = packageReferenceCode.Value
                        });
                    }                    
                    foreach (var packageReferenceServiceCode in dbPackageReferenceServiceCodes)
                    {
                        tempRefServCode.Add(new PackageReferenceServiceCodeDto(){
                            PackRateId = ratePlan.Id,
                            ReferenceCode = packageReferenceServiceCode.ReferenceCode,
                            ServiceId = packageReferenceServiceCode.ServiceId,
                            ServiceName = services.FirstOrDefault(x => x.Id == packageReferenceServiceCode.ServiceId).Name
                        });
                    }                    
                    foreach (PackageReferenceExtendCode packageReferenceExtendCode in dbPackageReferenceExtendCodes)
                    {
                        tempRefExtCode.Add(new PackageReferenceExtendCodeDto(){
                            PackRateId = ratePlan.Id,
                            ReferenceCode = packageReferenceExtendCode.ReferenceCode,
                            Months = packageReferenceExtendCode.Months
                        });
                    }
                }
            return new PackageRatePlanDto()
                                            {
                                                Id = ratePlan.Id,
                                                PackageId = ratePlan.PackageId,
                                                DurationInMonths = ratePlan.DurationInMonths,
                                                Price = ratePlan.Price,
                                                CurrencyId = ratePlan.CurrencyId,
                                                PackageReferenceCode = tempRefCode,
                                                PackageReferenceExtendCode = tempRefExtCode,
                                                PackageReferenceServiceCode = tempRefServCode
                                            };
        }
        public void ReCurrentPayment(){
            try
            {
                _subscribeService.ReCurrentPayment();      
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public PackageDashboard GetUserPackageDashboard(string token)
        {
            var functions = _subscribeService.GetSecurityFunctionsForUser(token); 

            PackageDashboard result = new PackageDashboard();
            result.packages = GetUserDashboardPackages(token);
            result.NumberOfSuppliers = _subscribeService.GetNumberOfSuppliers();
            result.MonthlyCode = functions.Contains(17) ? _subscribeService.GetMonthlyCode() : string.Empty;
            result.MorePackagesAvailable = _subscribeService.MorePackagesAvailable(token);

            return result;
        }

        public List<PackageDto> GetUserDashboardPackages(string token)
        {
            try
            {
                var user = _subscribeService.GetUserByToken(token);
                var packages = _subscribeService.GetAllPackages();
                var packageDto = new List<PackageDto>();
                var services = GetAllServices();
                foreach (var item in packages)
                {
                    var pack = ToDtoPackage(item, services, true);
                    var subscription = _subscribeService.GetSubscriptionForPackage(item.Id, user.Id);
                    if (item.StatusId == Statuses.Package.Published)
                    {
                        if (subscription != null)
                        {
                            pack.ValidTo = subscription.ExpirationDate;
                            foreach (var packageRatePlan in pack.PackageRatePlans)
                            {
                                if (packageRatePlan.Id == subscription.PackageRatePlanId && subscription.StatusId == Statuses.Subscription.Active)
                                    packageRatePlan.Bought = true;
                            }
                            packageDto.Add(pack);
                        }
                    }
                    else
                    {
                        if (subscription != null)
                        {
                            pack.ValidTo = subscription.ExpirationDate;
                            packageDto.Add(pack);
                        }
                    }
                }
                return packageDto;
            }
            catch (Exception ex)
            {
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
                throw ex;
            }
        }

        public AvailableEmailBroadcast GetNumberOfEmailBroadcast(string token)
        {
            return _subscribeService.GetNumberOfEmailBroadcast(token);
        }

        public void ChargeAll41()   //DELETE!!!!!!!!!!!!!!!!!!!!!!!!
        {
            List<int> invoices = new List<int>()
            {};

            foreach (var item in invoices)
                CreateTransaction(item);
        }
    }
}
