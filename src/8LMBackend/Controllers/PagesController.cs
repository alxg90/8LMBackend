using _8LMBackend.DataAccess.Models;
using _8LMBackend.DataAccess.DtoModels;
using _8LMBackend.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using _8LMBackend.Service.DTO;
using Microsoft.AspNetCore.Http.Internal;

namespace _8LMCore.Controllers
{
    public class PagesController : Controller
    {
        private readonly IPagesService _pagesService;

        public PagesController(IPagesService pagesService)
        {
            _pagesService = pagesService;
        }
        
        [HttpPost]
        public JsonResult NewLandingPage(string token)
        {
            try
            {
                return Json(new { status = "OK", PageID = _pagesService.NewLandingPage(token) });
            }
            catch (System.Exception ex)
            {

                return Json(new { status = "failed", error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult NewEmailPage(string token)
        {
            try
            {
                return Json(new { status = "OK", PageID = _pagesService.NewEmailPage(token) });
            }
            catch (System.Exception ex)
            {

                return Json(new { status = "failed", error = ex.Message });
            }
        }

        public Pages GetPage(int id, string token)
        {
            return _pagesService.GetPage(id);
        }

        public PageControl GetPageControl(int id, string token)
        {
            return _pagesService.GetPageControl(id);
        }

        public ActionResult DownloadPage(int pageID)
        {
            var page = _pagesService.GetPage(pageID);
            return File(_pagesService.Download(page), "application/zip", "page" + page.Id.ToString() + ".zip");
        }

        [HttpPost]
        public ActionResult UpdatePage([FromBody]dtoPage page, string token)
        {
            try
            {
                _pagesService.UpdatePage(page, token);
                return Json(new { status = "OK"});
            }
            catch (System.Exception ex)
            {

                return Json(new { status = "failed", error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdatePageMeta([FromBody]dtoPage page, string token)
        {
            try
            {
                _pagesService.UpdatePageMeta(page, token);
                return Json(new { status = "OK" });
            }
            catch (System.Exception ex)
            {

                return Json(new { status = "failed", error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DeletePage([FromBody]dtoPage page, string token)
        {
            try
            {
                _pagesService.DeletePage(page, token);
                return Json(new { status = "OK" });
            }
            catch (System.Exception ex)
            {

                return Json(new { status = "failed", error = ex.Message });
            }
        }

        public dtoPage[] GetLandingPages(string token)
        {
            return _pagesService.GetLandingPages(token).ToArray();
        }

        public dtoPage[] GetEmailPages(string token)
        {
            return _pagesService.GetEmailPages(token).ToArray();
        }

        [HttpPost]
        public ActionResult ActivatePage([FromBody]dtoPage page, string token)
        {
            try
            {
                _pagesService.Activate(page, token);
                return Json(new { status = "OK" });
            }
            catch (System.Exception ex)
            {

                return Json(new { status = "failed", error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DeactivatePage([FromBody]dtoPage page, string token)
        {
            try
            {
                _pagesService.Deactivate(page, token);
                return Json(new { status = "OK" });
            }
            catch (System.Exception ex)
            {

                return Json(new { status = "failed", error = ex.Message });
            }
        }

        public ActionResult HTML(int id)
        {
            try
            {
                var html = _pagesService.HTML(id);
                html = html.Replace("class=\"elm preview\"", "class=\"elm webview\"");
                ViewBag.html = html;
                return View();
            }
            catch (Exception ex)
            {
                return ViewBag.html = ex.Message;
            }
        }
        
        public dtoPage[] GetControls(int ParentID, string token)
        {
            return _pagesService.GetControls(ParentID, token).ToArray();
        }

        [HttpGet]
        public JsonResult GetThemes(string token){
            var status = "ok";
            var message = "";
            List<dtoPage> data = null;
            try {
                data = _pagesService.GetThemes(token, Types.Pages.Theme);
            }
            catch(Exception ex) {
                status = "fail";
                message = ex.Message;
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
            }
            return Json(new {status, message, data});
        }

        [HttpGet]
        public JsonResult GetEmailThemes(string token){
            var status = "ok";
            var message = "";
            List<dtoPage> data = null;
            try {
                data = _pagesService.GetThemes(token, Types.Pages.EmailTheme);
            }
            catch(Exception ex) {
                status = "fail";
                message = ex.Message;
                _8LMBackend.Logger.SaveLog(ex.StackTrace);
            }
            return Json(new {status, message, data});
        }

        public JsonResult GetDefaultLandingPageTemplateID()
        {
            var status = "ok";
            var message = "";
            int data = 0;
            try
            {
                data = _pagesService.GetDefaultLandingPageTemplateID();
            }
            catch (Exception ex)
            {
                status = "fail";
                message = ex.Message;
            }

            return Json(new { status, message, data });
        }

        public JsonResult GetDefaultEmailTemplateID()
        {
            var status = "ok";
            var message = "";
            int data = 0;
            try
            {
                data = _pagesService.GetDefaultEmailTemplateID();
            }
            catch (Exception ex)
            {
                status = "fail";
                message = ex.Message;
            }

            return Json(new { status, message, data });
        }

        [HttpPost("UploadFiles")]
        public void UploadImage(FormFile file, string token)
        {
            int UserID = _pagesService.GetUserID(token);
            string dir = "Gallery/" + UserID.ToString();

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            using (var stream = new FileStream(dir + "/12345.tmp", FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

        [HttpPost]
        public void UploadImage1(FormFile file, string token)
        {
            int UserID = _pagesService.GetUserID(token);
            string dir = "Gallery/" + UserID.ToString();

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            using (var stream = new FileStream(dir + "/12345.tmp", FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
    }
}
