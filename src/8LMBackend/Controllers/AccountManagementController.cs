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

        public JsonResult AccountList(string access_token)
        {
            try
            {
                var accounts = _accountManagementService.AccountList(access_token);
                return Json(new { accounts });
            }
            catch (System.Exception ex)
            {

                return Json(new { status = "failed", error = ex.InnerException.Message });
            }
        }

		public JsonResult AssignFunction(int FunctionID, int RoleID, string access_token)
		{
            try
            {
                _accountManagementService.AssignFunction(FunctionID, RoleID, access_token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message});
            }

            return Json(new { status = "ok"});
        }

		public JsonResult DeassignFunction(int FunctionID, int RoleID, string access_token)
		{
            try
            {
                _accountManagementService.DeassignFunction(FunctionID, RoleID, access_token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult UpdateCode(int yyyy, int mm, string Code, string access_token)
        {
            try
            {
                _accountManagementService.UpdateCode(yyyy, mm, Code, access_token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult GetCodes(string access_token)
        {
            try
            {
                var result = _accountManagementService.GetCodes(access_token);
                return Json(new { result });
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }
        }

        public JsonResult DeletePromoCode(int yyyy, int mm, string access_token)
        {
            try
            {
                _accountManagementService.DeletePromoCode(yyyy, mm, access_token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult CodesBulkUpdate(List<Promocode> codes, string access_token)
        {
            try
            {
                _accountManagementService.CodesBulkUpdate(codes, access_token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult GetPromoSuppliers(string access_token)
        {
            try
            {
                var result = _accountManagementService.GetPromoSuppliers(access_token);
                return Json(new { result });
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }
        }

        public JsonResult UpdatePromoUser(PromoUserViewModel u, string access_token)
        {
            try
            {
                _accountManagementService.UpdatePromoUser(u, access_token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult GetFunctionsForUser(string access_token)
        {
            try
            {
                var result = _accountManagementService.GetFunctionsForUser(access_token);
                return Json(new { result });
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }
        }

        public JsonResult CreateSecurityRole(string Name, string Description, string access_token)
        {
            try
            {
                _accountManagementService.CreateSecurityRole(Name, Description, access_token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult UpdateSecurityRole(int ID, string Name, string Description, string access_token)
        {
            try
            {
                _accountManagementService.UpdateSecurityRole(ID, Name, Description, access_token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult DeleteSecurityRole(int ID, string access_token)
        {
            try
            {
                _accountManagementService.DeleteSecurityRole(ID, access_token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult AssignRole(int UserID, int RoleID, string access_token)
        {
            try
            {
                _accountManagementService.AssignRole(UserID, RoleID, access_token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }

            return Json(new { status = "ok" });
        }

        public JsonResult DeassignRole(int UserID, int RoleID, string access_token)
        {
            try
            {
                _accountManagementService.DeassignRole(UserID, RoleID, access_token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.InnerException.Message });
            }

            return Json(new { status = "ok" });
        }
    }
}
