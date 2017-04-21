using System;
using System.Collections.Generic;
using System.Linq;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using _8LMBackend.DataAccess.Models;
using _8LMBackend.DataAccess.DtoModels;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _8LMBackend.Service
{
    public class SubscribeService : ServiceBase, ISubscribeService
    {
		public SubscribeService(IDbFactory dbFactory)
			: base(dbFactory) 
		{
		}
        private static readonly HttpClient client = new HttpClient();
        public void SavePackage(Package package){
            DbContext.Package.Add(package);
            DbContext.SaveChanges();
        }
        public void DeletePackage(int id)
        {
            var package = DbContext.Package.FirstOrDefault(x => x.Id == id);
            if(package.StatusId==Statuses.Package.New)
            {
                DbContext.PackageService.RemoveRange(DbContext.PackageService.Where(x=>x.PackageId==package.Id));
                DbContext.Invoice.RemoveRange(DbContext.Invoice.Where(x => x.PackageId == package.Id));
                DbContext.PackageRatePlan.RemoveRange(DbContext.PackageRatePlan.Where(p => p.PackageId == id));
                DbContext.Subscription.RemoveRange(DbContext.Subscription.Include("PackageRatePlan").Where(x => x.PackageRatePlan.PackageId == package.Id));
                DbContext.PackageReferenceCode.RemoveRange(DbContext.PackageReferenceCode.Where(x => x.PackageRatePlan.PackageId == package.Id));
                DbContext.PackageReferenceExtendCode.RemoveRange(DbContext.PackageReferenceExtendCode.Where(x => x.PackageRatePlan.PackageId == package.Id));
                DbContext.PackageReferenceServiceCode.RemoveRange(DbContext.PackageReferenceServiceCode.Where(x => x.PackageRatePlan.PackageId == package.Id));
                DbContext.Package.Remove(package);
                DbContext.SaveChanges();
            }
            else
            {
                throw new Exception();
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
        public Invoice PrepareInvoice(int PackageRateID, string token, string ReferenceCode = null){
            var user = GetUserByToken(token);

            var invoice = new Invoice();
            var packageRatePlan = DbContext.PackageRatePlan.FirstOrDefault(x => x.Id == PackageRateID);
            invoice.Amount = packageRatePlan.Price;
            invoice.PackageId = packageRatePlan.PackageId;
            
            var tempDiscount = DbContext.PackageReferenceCode.FirstOrDefault(x => x.PackageRatePlanId == packageRatePlan.Id && x.ReferenceCode == ReferenceCode);
            invoice.Discount = tempDiscount == null ? 0 : tempDiscount.IsFixed ? tempDiscount.Value : invoice.Amount * tempDiscount.Value / 100;
            
            invoice.AmountDue = invoice.Amount - invoice.Discount ;
            var subscriptions = DbContext.Subscription.Where(x => x.UserId == user.Id).ToList();
            if(subscriptions.Count == 0){
                invoice.AmountDue = DbContext.PaymentSetting.First().WelcomePackagePrice;
            }
            invoice.UserId = user.Id;                        
            invoice.StatusId = Statuses.Invoice.New;
            invoice.CreatedDate = DateTime.UtcNow;
            invoice.UpdatedDate = null;
            DbContext.Invoice.Add(invoice);
            DbContext.SaveChanges();
            return invoice;
        }
        public void AcceptPayment(RelayAuthorizeNetresponse rel){

            DbContext.RelayAuthorizeNetresponse.Add(rel);
            DbContext.SaveChanges();

            var invoice = DbContext.Invoice.FirstOrDefault(x=>x.Id == rel.InvoiceId);
            var packageRatePlan = DbContext.PackageRatePlan.FirstOrDefault(x=>x.PackageId == invoice.PackageId);
            var subsr = new Subscription();
            subsr.UserId = invoice.UserId;
            subsr.CreatedDate = DateTime.UtcNow;
            subsr.EffectiveDate = DateTime.UtcNow;
            subsr.ExpirationDate = subsr.EffectiveDate.AddMonths(packageRatePlan.DurationInMonths);
            subsr.PackageRatePlanId = packageRatePlan.Id;
            subsr.RelayAuthorizeNetresponse = rel.Id;
            subsr.StatusId = Statuses.Subscription.Active;

            invoice.StatusId = Statuses.Invoice.Captured;
            DbContext.Invoice.Update(invoice);

            DbContext.Subscription.Add(subsr);
            DbContext.SaveChanges();
        }
        public Invoice AcceptGuestPayment(RelayAuthorizeNetresponse rel){
            var invoice = DbContext.Invoice.FirstOrDefault(x=>x.Id == rel.InvoiceId);
            var user = new Users();
            user.Login = rel.XEmail;
            user.TypeId = Types.Users.User;
            user.StatusId = Statuses.Users.PaymentIssue;
            user.CreatedDate = DateTime.UtcNow;
            user.CreatedBy = 1;
            DbContext.Users.Add(user);     
            DbContext.SaveChanges();

            invoice.UserId = user.Id;
            DbContext.Invoice.Update(invoice);
            DbContext.SaveChanges();

            return invoice;
        }
        public Users GetUserByToken(string token){
            var userToken = DbContext.UserToken.Where(p => p.Token == token).FirstOrDefault();
            var user = DbContext.Users.FirstOrDefault(x => x.Id == userToken.UserId);
            return user;
        }
        public UserToken GetUserToken(string token){
            var userToken = DbContext.UserToken.Where(p => p.Token == token).FirstOrDefault();
            return userToken;
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
            package.StatusId = setActual;
            DbContext.Package.Update(package);
            DbContext.SaveChanges();
        }
        public List<PackageReferenceCode> GetPackageReferenceCodeById(int packRateId){
            return DbContext.PackageReferenceCode.Where(x=>x.PackageRatePlan.Id == packRateId).ToList();
        }
        public List<PackageReferenceExtendCode> GetPackageReferenceExtendCodeById(int packRateId){
            return DbContext.PackageReferenceExtendCode.Where(x=>x.PackageRatePlan.Id == packRateId).ToList();
        }  
        public List<PackageReferenceServiceCode> GetPackageReferenceServiceCodeById(int packRateId){
            return DbContext.PackageReferenceServiceCode.Where(x=>x.PackageRatePlan.Id == packRateId).ToList();
        }    
        public List<Package> GetUserPackages(int UserId)
        {
            return DbContext.Subscription.Include("PackageRatePlan").Where(x => x.UserId == UserId && x.StatusId == Statuses.Subscription.Active)
            .Join(DbContext.Package, s => s.PackageRatePlan.PackageId, p => p.Id, (s, p) => p).Distinct().ToList();
        }
        public Subscription GetSubscriptionForPackage(int packageId, int userId)
        {
            return DbContext.Subscription.Include("PackageRatePlan").FirstOrDefault(p => p.UserId == userId && p.PackageRatePlan.PackageId == packageId);
        }      
        public string GetTokenByInvoice(Invoice invoice){
            return DbContext.UserToken.FirstOrDefault(x=>x.UserId == invoice.UserId).Token;
        }
        public List<Package> GetActivePackages(){
            return DbContext.Package.Where(x=>x.StatusId == Statuses.Package.Published).ToList();
        }
        public async Task SaveCustomerProfile(int? userId, long transactionID){

            var paymentSettings = DbContext.PaymentSetting.First();
            var content = new JsonContent(new{createCustomerProfileFromTransactionRequest = new{merchantAuthentication = new{ name = paymentSettings.AuthorizeNetlogin, transactionKey = paymentSettings.AuthorizeNettransactionKey}, transId = transactionID.ToString()}});

            var requestTask = client.PostAsync("https://apitest.authorize.net/xml/v1/request.api", content);

            await requestTask.ContinueWith(t =>
            {
                using(var context = new DashboardDbContext())
                {
                    var responseString = t.Result.Content.ReadAsStringAsync().Result;
                    CustomerProfileResponseDto responseFromApi = JsonConvert.DeserializeObject<CustomerProfileResponseDto>(responseString);

                    AuthorizeNetcustomerProfile authProfile = new AuthorizeNetcustomerProfile();
                    authProfile.CreatedDate = DateTime.UtcNow;
                    authProfile.CustomerProfileId = responseFromApi.customerProfileId;
                    authProfile.UserId = userId.Value;
                    authProfile.PaymentProfileId = responseFromApi.customerPaymentProfileIdList[0] != null ? responseFromApi.customerPaymentProfileIdList[0] : 0;
                    context.AuthorizeNetcustomerProfile.Add(authProfile);
                    context.SaveChanges();
                }
            });
        }
        public async Task СreateTransactionRequest(long customerProfileId, long paymentProfileId, int invoiceId){
            var amountDue = DbContext.Invoice.FirstOrDefault(x => x.Id == invoiceId).AmountDue;
            var paymentSettings = DbContext.PaymentSetting.First();
            var content = new JsonContent(
                new{createTransactionRequest = 
                    new{merchantAuthentication = 
                        new{ name = paymentSettings.AuthorizeNetlogin,
                             transactionKey = paymentSettings.AuthorizeNettransactionKey}, 
                    transactionRequest = new{
                        transactionType = "authCaptureTransaction",
                        amount = amountDue,
                        profile = new{
                            customerProfileId = customerProfileId.ToString(),
                            paymentProfile = new{
                                paymentProfileId = paymentProfileId.ToString()
                            }
                        },
                        order = new{
                            invoiceNumber = invoiceId.ToString()
                        }
                    }
                    }});

            var requestTask = client.PostAsync("https://apitest.authorize.net/xml/v1/request.api", content);

            await requestTask.ContinueWith(t =>
            {
                using(var context = new DashboardDbContext())
                {
                    var responseString = t.Result.Content.ReadAsStringAsync().Result;
                    CreateTransactionResponseDto responseFromApi = JsonConvert.DeserializeObject<CreateTransactionResponseDto>(responseString);

                    AuthorizeNettransaction authNet = new AuthorizeNettransaction();
                    authNet.InvoiceId = invoiceId;
                    authNet.MerchantName = paymentSettings.AuthorizeNetlogin;
                    authNet.MerchantTransactionKey = paymentSettings.AuthorizeNettransactionKey;
                    authNet.CustomerProfileId = customerProfileId;
                    authNet.PaymentProfileId = paymentProfileId;
                    authNet.TransactionType = "authCaptureTransaction";
                    authNet.Amount = amountDue;
                    authNet.Description = responseFromApi.transactionResponse.messages.description;
                    authNet.CreatedDate = DateTime.UtcNow;
                    authNet.ResponseResultCode = responseFromApi.messages.resultCode;
                    authNet.ResponseCode = null;
                    authNet.ResponseText = responseFromApi.messages.message[0]["text"];
                    authNet.TransactionResponseResponseCode = responseFromApi.transactionResponse.responseCode;
                    authNet.TransactionResponseAuthCode = responseFromApi.transactionResponse.authCode;
                    authNet.TransactionResponseAvsresultCode = responseFromApi.transactionResponse.avsResultCode;
                    authNet.TransactionResponseCvvresultCode = responseFromApi.transactionResponse.cvvResultCode;
                    authNet.TransactionResponseCavvresultCode = responseFromApi.transactionResponse.cavvResultCode;
                    authNet.TransactionResponseTransId = responseFromApi.transactionResponse.transId;
                    authNet.TransactionResponseRefTransId = responseFromApi.transactionResponse.refTransID;
                    authNet.TransactionResponseTransHash = responseFromApi.transactionResponse.transHash;
                    authNet.TransactionResponseTestRequest = responseFromApi.transactionResponse.testRequest;
                    authNet.TransactionResponseAccountNumber = responseFromApi.transactionResponse.accountNumber;
                    authNet.TransactionResponseAccountType = responseFromApi.transactionResponse.accountType;
                    authNet.TransactionResponseMessageCode = responseFromApi.transactionResponse.messages.code;
                    authNet.TransactionResponseMessageDescription = responseFromApi.transactionResponse.messages.description;
                    
                    context.AuthorizeNettransaction.Add(authNet);
                    var invoice = DbContext.Invoice.FirstOrDefault(x => x.Id == invoiceId);
                    invoice.StatusId = Statuses.Invoice.New;
                    context.Invoice.Update(invoice);
                    context.SaveChanges();
                }
            });
        }
        public AuthorizeNetcustomerProfile GetAuthProfileByInvoice(int invoiceId){
            var invoice = DbContext.Invoice.FirstOrDefault( y=> y.Id == invoiceId);
            return DbContext.AuthorizeNetcustomerProfile.FirstOrDefault(x => x.UserId == invoice.UserId);
        }
        public async Task СaptureTransactionRequest(long customerProfileId, long paymentProfileId, int invoiceId){
            var amountDue = DbContext.Invoice.FirstOrDefault(x => x.Id == invoiceId).AmountDue;
            var transaction = DbContext.AuthorizeNettransaction.FirstOrDefault(x => x.InvoiceId == invoiceId);
            var paymentSettings = DbContext.PaymentSetting.First();
            var content = new JsonContent(
                new{createTransactionRequest = 
                    new{merchantAuthentication = 
                        new{ name = paymentSettings.AuthorizeNetlogin,
                             transactionKey = paymentSettings.AuthorizeNettransactionKey
                             }, 
                    transactionRequest = new{
                        transactionType = "priorAuthCaptureTransaction",
                        amount = amountDue,
                        refTranId = transaction.TransactionResponseRefTransId 
                    }
                    }});

            var requestTask = client.PostAsync("https://apitest.authorize.net/xml/v1/request.api", content);

            await requestTask.ContinueWith(t =>
            {
                using(var context = new DashboardDbContext())
                {
                    var responseString = t.Result.Content.ReadAsStringAsync().Result;
                    CreateTransactionResponseDto responseFromApi = JsonConvert.DeserializeObject<CreateTransactionResponseDto>(responseString);

                    AuthorizeNettransaction authNet = new AuthorizeNettransaction();
                    authNet.InvoiceId = invoiceId;
                    authNet.MerchantName = paymentSettings.AuthorizeNetlogin;
                    authNet.MerchantTransactionKey = paymentSettings.AuthorizeNettransactionKey;
                    authNet.CustomerProfileId = customerProfileId;
                    authNet.PaymentProfileId = paymentProfileId;
                    authNet.TransactionType = "priorAuthCaptureTransaction";
                    authNet.Amount = amountDue;
                    authNet.Description = responseFromApi.transactionResponse.messages.description;
                    authNet.CreatedDate = DateTime.UtcNow;
                    authNet.ResponseResultCode = responseFromApi.messages.resultCode;
                    authNet.ResponseCode = null;
                    authNet.ResponseText = responseFromApi.messages.message[0]["text"];
                    authNet.TransactionResponseResponseCode = responseFromApi.transactionResponse.responseCode;
                    authNet.TransactionResponseAuthCode = responseFromApi.transactionResponse.authCode;
                    authNet.TransactionResponseAvsresultCode = responseFromApi.transactionResponse.avsResultCode;
                    authNet.TransactionResponseCvvresultCode = responseFromApi.transactionResponse.cvvResultCode;
                    authNet.TransactionResponseCavvresultCode = responseFromApi.transactionResponse.cavvResultCode;
                    authNet.TransactionResponseTransId = responseFromApi.transactionResponse.transId;
                    authNet.TransactionResponseRefTransId = responseFromApi.transactionResponse.refTransID;
                    authNet.TransactionResponseTransHash = responseFromApi.transactionResponse.transHash;
                    authNet.TransactionResponseTestRequest = responseFromApi.transactionResponse.testRequest;
                    authNet.TransactionResponseAccountNumber = responseFromApi.transactionResponse.accountNumber;
                    authNet.TransactionResponseAccountType = responseFromApi.transactionResponse.accountType;
                    authNet.TransactionResponseMessageCode = responseFromApi.transactionResponse.messages.code;
                    authNet.TransactionResponseMessageDescription = responseFromApi.transactionResponse.messages.description;
                    
                    context.AuthorizeNettransaction.Add(authNet);
                    var invoice = DbContext.Invoice.FirstOrDefault(x => x.Id == invoiceId);
                    invoice.StatusId = Statuses.Invoice.Captured;
                    context.Invoice.Update(invoice);
                    context.SaveChanges();
                }
            });
        }
        public PackageRatePlan SavePackageRatePlan(PackageRatePlan packRatePlan){
            DbContext.PackageRatePlan.Add(packRatePlan);
            DbContext.SaveChanges();
            return packRatePlan;
        }
        public List<PackageRatePlan> GetPackageRatePlansByPackageID(int packageId){
            return DbContext.PackageRatePlan.Where(x => x.PackageId == packageId).ToList();
        }
    }
}
