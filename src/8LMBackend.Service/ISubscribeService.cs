using System.Collections.Generic;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Models;
using _8LMBackend.DataAccess.DtoModels;
using System;
using System.Threading.Tasks;

namespace _8LMBackend.Service
{
    public interface ISubscribeService
    {
        void SavePackage(Package package);
        void DeletePackage(int id);
        void UpdatePackage(Package package);
        _8LMBackend.DataAccess.Models.Service[] GetAllServices();
        Invoice PrepareInvoice(int PackageID, string token = null, string ReferenceCode = null);
        void AcceptPayment(RelayAuthorizeNetresponse rel);
        Package GetPackageById(int Id);
        Package[] GetAllPackages();
        Users GetUserByToken(string token);
        bool CheckPackageNameValid(string name, int? id);
        void SetActive(int id, int setActual);
        List<PackageReferenceCode> GetPackageReferenceCodeById(int packRateId);
        List<PackageReferenceExtendCode> GetPackageReferenceExtendCodeById(int packRateId);
        List<PackageReferenceServiceCode> GetPackageReferenceServiceCodeById(int packRateId);
        List<PackageService> GetPackageServicesById(int packageId);
        List<Package> GetUserPackages(int UserId);
        Subscription GetSubscriptionForPackage(int packageId, int userId);
        string GetTokenByInvoice(Invoice invoice);
        List<Package> GetActivePackages();
        // Invoice AcceptGuestPayment(RelayAuthorizeNetresponse rel);
        void SaveCustomerProfile(int? userId, long transactionID);
        Task СreateTransactionRequest(long customerProfileId, long paymentProfileId, int invoiceId);
        void СaptureTransactionRequest(long customerProfileId, long paymentProfileId, int invoiceId, int subscriptionId);
        AuthorizeNetcustomerProfile GetAuthProfileByInvoice(int invoiceId);
        PackageRatePlan SavePackageRatePlan(PackageRatePlan packRatePlan);
        List<PackageRatePlan> GetPackageRatePlansByPackageID(int packageId);
        UserToken GetUserToken(string token);
        void ReCurrentPayment();

        int GetNumberOfSuppliers();
        List<int> GetSecurityFunctionsForUser(string access_token);
        string GetMonthlyCode();
        bool MorePackagesAvailable(string access_token);
        AvailableEmailBroadcast GetNumberOfEmailBroadcast(string token);
        UpgradeSubscriptionResponse RequestUpgradeSubscription(string token, int TotalAmount);
        void UpgradePackage(string token, int CurrentRatePlanID, int NewRatePlanID, Guid UpgradeRequestID, int listID);
        string GetAuthorizeNETLogin();
        string GetSignatureKey();
    }
}
