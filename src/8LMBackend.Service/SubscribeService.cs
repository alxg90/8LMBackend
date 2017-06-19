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
using System.IO;

namespace _8LMBackend.Service
{
    public class SubscribeService : ServiceBase, ISubscribeService
    {
		public SubscribeService(IDbFactory dbFactory)
			: base(dbFactory) 
		{
		}
        private static readonly HttpClient client = new HttpClient();
        public void SavePackage(Package package)
        {
            DbContext.Package.Add(package);
            DbContext.SaveChanges();
        }
        public void DeletePackage(int id)
        {
            var package = DbContext.Package.FirstOrDefault(x => x.Id == id);
            if(package.StatusId==Statuses.Package.New)
            {
                DbContext.PackageService.RemoveRange(DbContext.PackageService.Where(x=>x.PackageId==package.Id));
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
        public void UpdatePackage(Package package)
        {
            DbContext.Package.Update(package);
            DbContext.SaveChanges();
        }

        public _8LMBackend.DataAccess.Models.Service[] GetAllServices()
        {
            var services = DbContext.Service.ToArray();
            return (services.Length > 0) ? services : new _8LMBackend.DataAccess.Models.Service[0];
        } 
        public Invoice PrepareInvoice(int PackageRateID, string token, string ReferenceCode = null)
        {
            var user = GetUserByToken(token);
                
            var invoice = new Invoice();
            var packageRatePlan = DbContext.PackageRatePlan.FirstOrDefault(x => x.Id == PackageRateID);
            invoice.Amount = packageRatePlan.Price;
            invoice.PackageRatePlanId = packageRatePlan.Id;
            
            var tempDiscount = DbContext.PackageReferenceCode.FirstOrDefault(x => x.PackageRatePlanId == packageRatePlan.Id && x.ReferenceCode == ReferenceCode);
            invoice.Discount = tempDiscount == null ? 0 : tempDiscount.IsFixed ? tempDiscount.Value : invoice.Amount * tempDiscount.Value / 100;            
            invoice.AmountDue = invoice.Amount - invoice.Discount;

            if ((DbContext.Subscription.Where(x => x.UserId == user.Id).ToList().Count == 0) && (user.StatusId != 8))
                invoice.AmountDue = DbContext.PaymentSetting.First().WelcomePackagePrice;

            invoice.UserId = user.Id;                        
            invoice.StatusId = Statuses.Invoice.New;
            invoice.CreatedDate = DateTime.UtcNow;
            invoice.UpdatedDate = null;
            DbContext.Invoice.Add(invoice);
            DbContext.SaveChanges();
            return invoice;
        }
        public void AcceptPayment(RelayAuthorizeNetresponse rel)
        {
            DbContext.RelayAuthorizeNetresponse.Add(rel);
            DbContext.SaveChanges();

            var invoice = DbContext.Invoice.FirstOrDefault(x=>x.Id == rel.InvoiceId);
            var packageRatePlan = DbContext.PackageRatePlan.FirstOrDefault(x=>x.Id == invoice.PackageRatePlanId); //packageId it's packageRatePlanId, should be renamed in database
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
        // public Invoice AcceptGuestPayment(RelayAuthorizeNetresponse rel){
        //     var invoice = DbContext.Invoice.FirstOrDefault(x=>x.Id == rel.InvoiceId);
        //     var user = new Users();
        //     user.Login = rel.XEmail;
        //     user.TypeId = Types.Users.User;
        //     user.StatusId = Statuses.Users.PaymentIssue;
        //     user.CreatedDate = DateTime.UtcNow;
        //     user.CreatedBy = 1;
        //     DbContext.Users.Add(user);     
        //     DbContext.SaveChanges();

        //     invoice.UserId = user.Id;
        //     DbContext.Invoice.Update(invoice);
        //     DbContext.SaveChanges();

        //     return invoice;
        // }
        public Users GetUserByToken(string token)
        {
            var userToken = DbContext.UserToken.Where(p => p.Token == token).FirstOrDefault();
            if(userToken==null)
                throw new Exception("User token doesen't exist");
            var user = DbContext.Users.FirstOrDefault(x => x.Id == userToken.UserId);
            return user;
        }
        public UserToken GetUserToken(string token)
        {
            var userToken = DbContext.UserToken.Where(p => p.Token == token).FirstOrDefault();
            return userToken;
        }        
        public Package GetPackageById(int id)
        {
            return DbContext.Package.FirstOrDefault(x => x.Id == id);
        }
        public Package[] GetAllPackages()
        {
            Package[] packages = DbContext.Package.ToArray();
            return packages;
        }
        public List<PackageService> GetPackageServicesById(int packageId)
        {
            return DbContext.PackageService.Where(x => x.PackageId == packageId).ToList();
        }

        public bool CheckPackageNameValid(string name, int? packageId)
        {
            var pack = DbContext.Package.FirstOrDefault(x => x.Name == name);
            if(packageId==0)
            {
                return pack == null;
            }
            else
            {
                if(pack == null || pack.Id == packageId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void SetActive(int id, int setActual)
        {
            var package = DbContext.Package.FirstOrDefault(x => x.Id == id);
            package.StatusId = setActual;
            DbContext.Package.Update(package);
            DbContext.SaveChanges();
        }
        public List<PackageReferenceCode> GetPackageReferenceCodeById(int packRateId)
        {
            return DbContext.PackageReferenceCode.Where(x=>x.PackageRatePlan.Id == packRateId).ToList();
        }
        public List<PackageReferenceExtendCode> GetPackageReferenceExtendCodeById(int packRateId)
        {
            return DbContext.PackageReferenceExtendCode.Where(x=>x.PackageRatePlan.Id == packRateId).ToList();
        }  
        public List<PackageReferenceServiceCode> GetPackageReferenceServiceCodeById(int packRateId)
        {
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
        public string GetTokenByInvoice(Invoice invoice)
        {
            return DbContext.UserToken.FirstOrDefault(x=>x.UserId == invoice.UserId).Token;
        }
        public List<Package> GetActivePackages()
        {
            return DbContext.Package.Where(x=>x.StatusId == Statuses.Package.Published).ToList();
        }
        public void SaveCustomerProfile(int? userId, long transactionID)
        {
            var paymentSettings = DbContext.PaymentSetting.First();
            var content = new JsonContent(new{
                                                createCustomerProfileFromTransactionRequest = new{
                                                    merchantAuthentication = new{ 
                                                        name = paymentSettings.AuthorizeNetlogin,
                                                        transactionKey = paymentSettings.AuthorizeNettransactionKey},
                                                    transId = transactionID.ToString()}});
            var requestTask = client.PostAsync("https://apitest.authorize.net/xml/v1/request.api", content);
            requestTask.ContinueWith(t =>
            {
                using(var context = new DashboardDbContext())
                {
                    var responseString = t.Result.Content.ReadAsStringAsync().Result;
                    CustomerProfileResponseDto responseFromApi = JsonConvert.DeserializeObject<CustomerProfileResponseDto>(responseString);
                    AuthorizeNetcustomerProfile authProfile = new AuthorizeNetcustomerProfile();
                    authProfile.CreatedDate = DateTime.UtcNow;
                    authProfile.CustomerProfileId = responseFromApi.customerProfileId;
                    authProfile.UserId = userId.Value;
                    authProfile.PaymentProfileId = responseFromApi.customerPaymentProfileIdList[0];
                    context.AuthorizeNetcustomerProfile.Add(authProfile);
                    context.SaveChanges();
                }
            });
        }
        public async Task СreateTransactionRequest(long customerProfileId, long paymentProfileId, int invoiceId)
        {
            var amountDue = DbContext.Invoice.FirstOrDefault(x => x.Id == invoiceId).AmountDue;
            var paymentSettings = DbContext.PaymentSetting.First();
            var content = new JsonContent(
                new{createTransactionRequest = 
                    new{merchantAuthentication = 
                        new { name = paymentSettings.AuthorizeNetlogin,
                             transactionKey = paymentSettings.AuthorizeNettransactionKey
                            },
                        refId = invoiceId.ToString(),
                    transactionRequest = new
                    {
                        transactionType = "authCaptureTransaction",
                        amount = (decimal)amountDue / (decimal)100,
                        profile = new
                        {
                            customerProfileId = customerProfileId.ToString(),
                            paymentProfile = new
                            {
                                paymentProfileId = paymentProfileId.ToString()
                            }
                        },
                        order = new
                        {
                            invoiceNumber = invoiceId.ToString()
                        }
                    }
                    }});
            //var requestTask = client.PostAsync("https://apitest.authorize.net/xml/v1/request.api", content);
            var requestTask = client.PostAsync("https://api.authorize.net/xml/v1/request.api", content);
            await requestTask.ContinueWith(t =>
            {
                var responseString = t.Result.Content.ReadAsStringAsync().Result;

                string s = invoiceId.ToString() + "," +
                           customerProfileId.ToString() + "," +
                           paymentProfileId.ToString() + "," +
                           ((decimal)amountDue / (decimal)100).ToString() + "," +
                           responseString;
                File.AppendAllText(@"c:\ELMCharging\responses.csv", s + Environment.NewLine);

                /*using(var context = new DashboardDbContext())
                {
                    var responseString = t.Result.Content.ReadAsStringAsync().Result;
                    responseString = responseString.Replace(@"\""", @"""");
                    responseString = responseString.Replace(@"{""transactionResponse"":", @"");
                    responseString = responseString.Replace(@"}]}}", @"}]}");
                    CreateTransactionResponseDto responseFromApi = JsonConvert.DeserializeObject<CreateTransactionResponseDto>(responseString);
                    AuthorizeNettransaction authNet = new AuthorizeNettransaction();
                    authNet.InvoiceId = invoiceId;
                    authNet.MerchantName = paymentSettings.AuthorizeNetlogin;
                    authNet.MerchantTransactionKey = paymentSettings.AuthorizeNettransactionKey;
                    authNet.CustomerProfileId = customerProfileId;
                    authNet.PaymentProfileId = paymentProfileId;
                    authNet.TransactionType = "authCaptureTransaction";
                    authNet.Amount = amountDue;
                    try
                    {
                        authNet.Description = responseFromApi.transactionResponse.messages.description;
                        authNet.TransactionResponseMessageCode = responseFromApi.transactionResponse.messages.code;
                        authNet.TransactionResponseMessageDescription = responseFromApi.transactionResponse.messages.description;
                        authNet.ResponseResultCode = responseFromApi.messages.First().resultCode;
                        authNet.ResponseText = responseFromApi.messages.First().message[0]["text"];
                        authNet.ResponseCode = responseFromApi.transactionResponse.responseCode.ToString();
                    }
                    catch
                    {
                        authNet.Description = "";
                        authNet.TransactionResponseMessageDescription = "";
                        authNet.ResponseResultCode = "";
                        authNet.ResponseText = "";
                        authNet.ResponseCode = "";
                    }
                    authNet.CreatedDate = DateTime.UtcNow;
                    
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
                    
                    context.AuthorizeNettransaction.Add(authNet);

                    var invoice = DbContext.Invoice.FirstOrDefault(x => x.Id == invoiceId);
                    invoice.StatusId = Statuses.Invoice.New;
                    context.Invoice.Update(invoice);
                    context.SaveChanges();
                }*/
            });
        }
        public AuthorizeNetcustomerProfile GetAuthProfileByInvoice(int invoiceId)
        {
            var invoice = DbContext.Invoice.FirstOrDefault( y=> y.Id == invoiceId);
            return DbContext.AuthorizeNetcustomerProfile.FirstOrDefault(x => x.UserId == invoice.UserId);
        }
        public void СaptureTransactionRequest(long customerProfileId, long paymentProfileId, int invoiceId, int subscriptionId)
        {
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
            requestTask.ContinueWith(t =>
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
                    authNet.ResponseResultCode = responseFromApi.messages.First().resultCode;
                    authNet.ResponseCode = null;
                    authNet.ResponseText = responseFromApi.messages.First().message[0]["text"];
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
                    if(responseFromApi.transactionResponse.responseCode == 1)
                    {
                        invoice.StatusId = Statuses.Invoice.Captured;
                        var subscription = DbContext.Subscription.FirstOrDefault(x => x.Id == subscriptionId);
                        subscription.ExpirationDate.AddMonths(DbContext.PackageRatePlan.FirstOrDefault(y=>y.Id == subscription.PackageRatePlanId).DurationInMonths);
                        DbContext.Subscription.Update(subscription);
                    }
                    else
                    {
                        var user = DbContext.Users.FirstOrDefault(x => x.Id == invoice.UserId);
                        user.StatusId = Statuses.Users.PaymentIssue;
                        DbContext.Users.Update(user);
                    }
                    context.Invoice.Update(invoice);
                    context.SaveChanges();
                }
            });
        }
        public PackageRatePlan SavePackageRatePlan(PackageRatePlan packRatePlan)
        {
            DbContext.PackageRatePlan.Add(packRatePlan);
            DbContext.SaveChanges();
            return packRatePlan;
        }
        public List<PackageRatePlan> GetPackageRatePlansByPackageID(int packageId)
        {
            return DbContext.PackageRatePlan.Where(x => x.PackageId == packageId).ToList();
        }
        public void ReCurrentPayment()
        {
            var subscriptions = DbContext.Subscription.Where(x => x.ExpirationDate.Date <= DateTime.UtcNow.Date).ToList();
            if (subscriptions != null)
            {
                foreach (var subscription in subscriptions)
                {
                    if(DbContext.Users.FirstOrDefault(x => x.Id == subscription.UserId).StatusId == Statuses.Users.Active)
                    {
                        var packRatePlan = DbContext.PackageRatePlan.FirstOrDefault(x=>x.Id == subscription.PackageRatePlanId);
                        var invoice = new Invoice();
                        invoice.Amount = packRatePlan.Price;
                        invoice.PackageRatePlanId = packRatePlan.Id;
                        
                        var tempDiscount = DbContext.PackageReferenceCode.FirstOrDefault(x => x.PackageRatePlanId == packRatePlan.Id && x.ReferenceCode == DbContext.PackageReferenceCode.FirstOrDefault(y=>y.PackageRatePlanId == packRatePlan.Id).ReferenceCode);
                        invoice.Discount = tempDiscount == null ? 0 : tempDiscount.IsFixed ? tempDiscount.Value : invoice.Amount * tempDiscount.Value / 100;            
                        invoice.AmountDue = invoice.Amount - invoice.Discount;
                        invoice.UserId = subscription.UserId;                        
                        invoice.StatusId = Statuses.Invoice.New;
                        invoice.CreatedDate = DateTime.UtcNow;
                        invoice.UpdatedDate = null;
                        DbContext.Invoice.Add(invoice);
                        DbContext.SaveChanges();

                        var customerProfile = DbContext.AuthorizeNetcustomerProfile.FirstOrDefault(x => x.UserId == subscription.UserId);
                        if(customerProfile!=null)
                        {              
                            this.СaptureTransactionRequest(customerProfile.CustomerProfileId, customerProfile.PaymentProfileId, invoice.Id, subscription.Id);
                        }
                        else
                        {
                            var firstInvoice = DbContext.Invoice.Where(x => x.UserId == subscription.UserId && x.StatusId == Statuses.Invoice.Captured).OrderByDescending(t => t.CreatedDate).FirstOrDefault();
                            var relay = DbContext.RelayAuthorizeNetresponse.FirstOrDefault(x => x.InvoiceId == firstInvoice.Id);
                            if(relay.XTransId != 0)
                            {
                                this.SaveCustomerProfile(subscription.UserId, relay.XTransId);
                                customerProfile = DbContext.AuthorizeNetcustomerProfile.FirstOrDefault(x => x.UserId == subscription.UserId);
                                this.СaptureTransactionRequest(customerProfile.CustomerProfileId, customerProfile.PaymentProfileId, invoice.Id, subscription.Id);
                            }
                        }
                    }
                }
            }
            var expiredSubscriptions = DbContext.Subscription.Where(x => x.ExpirationDate.Date <= DateTime.Now.Date && DbContext.Users.FirstOrDefault(y=>y.Id == x.UserId).StatusId == Statuses.Users.PaymentIssue).ToList();
            foreach(var expiredSubscription in expiredSubscriptions)
            {
                expiredSubscription.StatusId = Statuses.Subscription.Expired;
                DbContext.Subscription.Update(expiredSubscription);
            }
            DbContext.SaveChanges();
        }

        public int GetNumberOfSuppliers()
        {
            return DbContext.PromoSupplier.Count();
        }

        public string GetMonthlyCode()
        {
            var code = DbContext.PromoCode.FirstOrDefault(p => p.Yyyy == DateTime.UtcNow.Year && p.Mm == DateTime.UtcNow.Month);
            return code != null ? code.Code : string.Empty;
        }

        public List<int> GetSecurityFunctionsForUser(string access_token)
        {
            var u = DbContext.UserToken.Where(p => p.Token == access_token).FirstOrDefault();
            if (u == default(UserToken))
                throw new Exception("Not authorized");

            var userId = u.UserId;

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

            return result;
        }

        public bool MorePackagesAvailable(string access_token)
        {
            var result = GetSecurityFunctionsForUser(access_token);

            var ss = DbContext.PackageService.Include("Package").Where(p => p.Package.StatusId == Statuses.Package.Published).Select(p => p.ServiceId).Distinct().ToList();
            var sfs = ss.Join(DbContext.ServiceFunction, s => s, sf => sf.ServiceId, (s, sf) => sf).Select(p => p.SecurityFunctionId).Distinct().ToList();
            
            return result.Distinct().ToList().Count != sfs.Count;
        }
    }
}
