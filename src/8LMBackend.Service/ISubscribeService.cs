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
        List<PackageReferenceCode> GetPackageReferenceCodeById(int packageId);
        List<PackageReferenceExtendCode> GetPackageReferenceExtendCodeById(int packageId);
        List<PackageReferenceServiceCode> GetPackageReferenceServiceCodeById(int packageId);
        List<PackageService> GetPackageServicesById(int packageId);
        List<Package> GetUserPackages(int UserId);
        Subscription GetSubscriptionForPackage(int packageId, int userId);
        string GetTokenByInvoice(Invoice invoice);
        List<Package> GetActivePackages();
        Invoice AcceptGuestPayment(RelayAuthorizeNetresponse rel);
        Task SaveCustomerProfile(int? userId, long transactionID);
    }
}
