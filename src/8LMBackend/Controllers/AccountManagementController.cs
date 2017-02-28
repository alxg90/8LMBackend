using _8LMBackend.DataAccess.Models;
using _8LMBackend.Service;
using Microsoft.AspNetCore.Mvc;
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

        public void AddPromoCode(string Code, int dtFrom, int dtTo)
        {
            _accountManagementService.AddPromoCode(Code, dtFrom, dtTo);
        }

        public void AssignPromoCode(int UserID, string Code)
        {
            _accountManagementService.AssignPromoCode(UserID, Code);
        }

        public void DeassignPromoCode(int UserID)
        {
            _accountManagementService.DeassignPromoCode(UserID);
        }

        public JsonResult CodeList()
        {
            var result = _accountManagementService.CodeList();
            return Json(new { result });
        }

        public void UpdatePromoCode(int ID, string Code, int FromDate, int ToDate)
        {
            _accountManagementService.UpdatePromoCode(ID, Code, FromDate, ToDate);
        }

        public void DeletePromoCode(int ID)
        {
            _accountManagementService.DeletePromoCode(ID);
        }
    }
}
