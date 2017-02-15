using _8LMBackend.DataAccess.Models;
using _8LMBackend.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace _8LMCore.Controllers
{
    public class AccountManagementController : Controller
    {
        private readonly IAccountManagementService _accountManagementService;

        public AccountManagementController(IAccountManagementService accountManagementService)
        {
            _accountManagementService = accountManagementService;
        }

        public JsonResult AccountList()
        {
            var accounts = _accountManagementService.AccountList();
            return Json(new { accounts });
        }

		public void AssignFunction(int FunctionID, int RoleID, int CreatedBy)
		{
			_accountManagementService.AssignFunction(FunctionID, RoleID, CreatedBy);
		}

		public void DeassignFunction(int FunctionID, int RoleID)
		{
			_accountManagementService.DeassignFunction(FunctionID, RoleID);
		}

        public void AddPromoCode(string Code, DateTime dtFrom, DateTime dtTo)
        {
            _accountManagementService.AddPromoCode(Code, dtFrom, dtTo);
        }

        public void AssignPromoCode(int UserID, string Code)
        {
            _accountManagementService.AssignPromoCode(UserID, Code);
        }
    }
}
