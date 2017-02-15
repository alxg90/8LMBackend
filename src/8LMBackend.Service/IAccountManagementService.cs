using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _8LMBackend.Service.ViewModels;

namespace _8LMBackend.Service
{
    public interface IAccountManagementService
    {
		AccountManagementViewModel AccountList();
		void AssignFunction(int FunctionID, int RoleID, int CreatedBy);
		void DeassignFunction(int FunctionID, int RoleID);
        void AddPromoCode(string Code, DateTime dtFrom, DateTime dtTo);
        void AssignPromoCode(int UserID, string Code);
    }
}
