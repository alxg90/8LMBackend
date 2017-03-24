using System.Collections.Generic;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.Service
{
    public interface IAccountManagementService
    {
		AccountManagementViewModel AccountList(string access_token);
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
        void VerifyFunction(int FunctionID, string access_token);
        void DeletePromoUser(int ID, string token);
        AccountViewModel GetAccount(int id, string token);
        void UpdateUser(Users u, string token);
        void DeleteUser(Users u, string token);
    }
}
