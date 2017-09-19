using _8LMBackend.DataAccess.Models;
using _8LMBackend.Service;
using _8LMBackend.Service.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
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

        public JsonResult GetAccount(string token)
        {
            try
            {
                var account = _accountManagementService.GetAccount(token);
                return Json(new { account });
            }
            catch (System.Exception ex)
            {

                return Json(new { status = "failed", error = ex.Message });
            }
        }

        public JsonResult ExcludeEmailAddress(string token, string email)
        {
            try
            {
                _accountManagementService.ExcludeEmailAddress(token, email);
                return Json(new { status = "OK" });
            }
            catch (System.Exception ex)
            {

                return Json(new { status = "failed", error = ex.Message });
            }
        }

        //[HttpPost]
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

        //[HttpPost]
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

        [HttpPost]
        public JsonResult UpdateCode([FromBody]PromoCodeModel pc, string token)
        {
            try
            {
                _accountManagementService.UpdateCode(pc.yyyy, pc.mm, pc.Code, token);
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

        [HttpPost]
        public JsonResult CodesBulkUpdate([FromBody]List<PromoCode> codes)
        {
            try
            {
                _accountManagementService.CodesBulkUpdate(codes, Request.Query["token"]);
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

        [HttpPost]
        public JsonResult UpdatePromoUser([FromBody]PromoUserViewModel u)
        {
            try
            {
                _accountManagementService.UpdatePromoUser(u, Request.Query["token"]);
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


        //[HttpPost]
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

        //[HttpPost]
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

        //[HttpPost]
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

        //[HttpPost]
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

        //[HttpPost]
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

        //[HttpPost]
        public JsonResult DeletePromoUser(int ID, string token)
        {
            try
            {
                _accountManagementService.DeletePromoUser(ID, token);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }

            return Json(new { status = "ok" });
        }

        [HttpPost]
        public JsonResult UpdateUser([FromBody]AccountViewModel u)
        {
            try
            {
                _accountManagementService.UpdateUser(u, Request.Query["token"]);
            }
            catch (System.Exception ex)
            {
                return Json(new { status = "failed", error = ex.Message });
            }

            return Json(new { status = "ok" });
        }

        public ActionResult DownloadSupplierPDF(string token)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                var f = _accountManagementService.DownloadSupplierPDF(token);
                f.CopyTo(ms);
                ms.Position = 0;
                return File(ms, "application/pdf", "CorePricing.pdf");
            }
            catch (System.Exception ex)
            {

                return Json(new { status = "failed", error = ex.Message });
            }
        }
    }
}
