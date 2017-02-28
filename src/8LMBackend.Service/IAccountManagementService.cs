using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.Service
{
    public interface IAccountManagementService
    {
		AccountManagementViewModel AccountList();
		void AssignFunction(int FunctionID, int RoleID, int CreatedBy);
		void DeassignFunction(int FunctionID, int RoleID);
        void AddPromoCode(string Code, int dtFrom, int dtTo);
        void AssignPromoCode(int UserID, string Code);
        void DeassignPromoCode(int UserID);
        List<Promocode> CodeList();
        void UpdatePromoCode(int ID, string Code, int FromDate, int ToDate);
        void DeletePromoCode(int ID);
    }
}
