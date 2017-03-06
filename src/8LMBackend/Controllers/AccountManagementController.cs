using _8LMBackend.DataAccess.Models;
using _8LMBackend.Service;
using _8LMBackend.Service.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public JsonResult AccountList(string token)
        {
            try
            {
                var accounts = _accountManagementService.AccountList(token);
                return Json(new { accounts });
            }
            catch (System.Exception ex)
            {

                return Json(new { status = "failed", error = ex.Message });
            }
        }

		public JsonResult AssignFunction(int FunctionID, int RoleID, string token)
		{
            try
            {
                _accountManagementService.AssignFunction(FunctionID, RoleID, token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message});
            }

            return Json(new { status = "ok"});
        }

		public JsonResult DeassignFunction(int FunctionID, int RoleID, string token)
		{
            try
            {
                _accountManagementService.DeassignFunction(FunctionID, RoleID, token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult UpdateCode(int yyyy, int mm, string Code, string token)
        {
            try
            {
                _accountManagementService.UpdateCode(yyyy, mm, Code, token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult GetCodes(string token)
        {
            try
            {
                var result = _accountManagementService.GetCodes(token);
                return Json(new { result });
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }
        }

        public JsonResult DeletePromoCode(int yyyy, int mm, string token)
        {
            try
            {
                _accountManagementService.DeletePromoCode(yyyy, mm, token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult CodesBulkUpdate(List<Promocode> codes, string token)
        {
            try
            {
                _accountManagementService.CodesBulkUpdate(codes, token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult GetPromoSuppliers(string token)
        {
            try
            {
                var result = _accountManagementService.GetPromoSuppliers(token);
                return Json(new { result });
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }
        }

        public JsonResult UpdatePromoUser(PromoUserViewModel u, string token)
        {
            try
            {
                _accountManagementService.UpdatePromoUser(u, token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult GetFunctionsForUser(string token)
        {
            try
            {
                var result = _accountManagementService.GetFunctionsForUser(token);
                return Json(new { result });
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }
        }

        public JsonResult CreateSecurityRole(string Name, string Description, string token)
        {
            try
            {
                var id = _accountManagementService.CreateSecurityRole(Name, Description, token);
                return Json(new { ID = id.ToString() });
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }
        }

        public JsonResult UpdateSecurityRole(int ID, string Name, string Description, string token)
        {
            try
            {
                _accountManagementService.UpdateSecurityRole(ID, Name, Description, token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult DeleteSecurityRole(int ID, string token)
        {
            try
            {
                _accountManagementService.DeleteSecurityRole(ID, token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult AssignRole(int UserID, int RoleID, string token)
        {
            try
            {
                _accountManagementService.AssignRole(UserID, RoleID, token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult DeassignRole(int UserID, int RoleID, string token)
        {
            try
            {
                _accountManagementService.DeassignRole(UserID, RoleID, token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }

            return Json(new { status = "ok" });
        }
    }
}
