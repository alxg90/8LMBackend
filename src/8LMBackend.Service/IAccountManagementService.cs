using System.Collections.Generic;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Models;
using System.IO;

namespace _8LMBackend.Service
{
    public interface IAccountManagementService
    {
        List<AccountViewModel> AccountList(string access_token);
        AccountViewModel GetAccount(string access_token);
        void ExcludeEmailAddress(string access_token, string email);
        void AssignFunction(int FunctionID, int RoleID, string access_token);
		void DeassignFunction(int FunctionID, int RoleID, string access_token);
        void UpdateCode(int yyyy, int mm, string Code, string access_token);
        List<PromoCode> GetCodes(string access_token);
        void DeletePromoCode(int yyyy, int mm, string access_token);
        void CodesBulkUpdate(List<PromoCode> codes, string access_token);
        List<PromoUserViewModel> GetPromoSuppliers(string access_token);
        void UpdatePromoUser(PromoUserViewModel u, string access_token);
        List<int> GetFunctionsForUser(string access_token);
        int CreateSecurityRole(string Name, string Description, string access_token);
        void UpdateSecurityRole(int ID, string Name, string Description, string access_token);
        void DeleteSecurityRole(int ID, string access_token);
        void AssignRole(int UserID, int RoleID, string access_token);
        void DeassignRole(int UserID, int RoleID, string access_token);
        int GetUserID(string access_token);
        bool VerifyFunction(int FunctionID, string access_token);
        void DeletePromoUser(int ID, string token);
        void UpdateUser(AccountViewModel u, string token);
        FileStream DownloadSupplierPDF(string token);
    }
}
