using _8LMBackend.DataAccess.Models;
using _8LMBackend.DataAccess.DtoModels;
using _8LMBackend.Service;
using _8LMBackend.DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using _8LMBackend.Service.DTO;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;

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

        [HttpPost]
        public ActionResult UploadImage(IFormFile file, int TypeID, string title, string token)
        {
            try
            {
                int UserID = _pagesService.GetUserID(token);
                string dir = "Gallery/" + UserID.ToString();
                string cn = $"{Guid.NewGuid()}-{file.FileName.Replace(" ", "-")}";

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (var stream = new FileStream(dir + "/" + cn, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                Gallery item = new Gallery()
                {
                    UserID = UserID,
                    TypeID = TypeID,
                    Title = title,
                    FileName = file.FileName,
                    CurrentName = cn
                };
                _pagesService.SaveGallery(item, token);

                return Json(new { status = "OK" });
            }
            catch (System.Exception ex)
            {
                return Json(new
                {
                    status = "failed",
                    error = ex.Message
                });
            }
        }

        public JsonResult GetGalleryList(int ItemType, int PageCapacity, int PageNumber, string search, string token)
        {
            var status = "ok";
            var message = "";
            GalleryViewModel data = null;
            try
            {
                data = _pagesService.GetGalleryList((GalleryType)ItemType, PageCapacity, PageNumber, search, token);
            }
            catch (Exception ex)
            {
                status = "fail";
                message = ex.Message;
            }

            return Json(new { status, message, data });
        }

        public ActionResult DownloadImage(int ID, string token)
        {
            var image = _pagesService.GetGallery(ID, token);

            int UserID = _pagesService.GetUserID(token);
            string dir = "Gallery/" + UserID.ToString();

            MemoryStream ms = new MemoryStream();
            using (var stream = new FileStream(dir + "/" + image.CurrentName, FileMode.Open))
            {
                stream.CopyTo(ms);
            }

            ms.Position = 0;

            Extention ext = new Extention();
            return File(ms, ext.Get(Path.GetExtension(image.FileName)), image.FileName);
        }

        public void MigratePageControlsFromDevToProd()
        {
            List<int> excludeIDs = new List<int>()
            {
                4696
            };
            string replaceFrom = "http://ang.mark8.media/";
            string replaceTo = "https://app.eightlegged.media/";

            List<PageControl> controls;
            using (DashboardDbContext db = new DashboardDbContext())
            {
                controls = db.PageControl.ToList();
            }

            using (ProductionDbContext db = new ProductionDbContext())
            {
                foreach (var c in controls)
                {
                    if (!excludeIDs.Contains(c.Id))
                    {
                        if (c.Json != null)
                            c.Json.Replace(replaceFrom, replaceTo);

                        if (c.PreviewUrl != null)
                        c.PreviewUrl.Replace(replaceFrom, replaceTo);
                            db.Set<PageControl>().Add(c);
                    }
                }
                db.SaveChanges();
            }
        }
        
        public void ParseEmailList()
        {
            throw new NotImplementedException();
        }

        public JsonResult RemoveGalleryItem(int ID, string token)
        {
            var status = "ok";
            var message = "";
            try
            {
                _pagesService.RemoveGalleryItem(ID, token);
            }
            catch (Exception ex)
            {
                status = "fail";
                message = ex.Message;
            }

            return Json(new { status, message});
        }

        [HttpPost]
        public JsonResult UpdateGalleryItem([FromBody]Gallery item, string token)
        {
            var status = "ok";
            var message = "";
            try
            {
                _pagesService.UpdateGalleryItem(item.ID, item.Title, token);
            }
            catch (Exception ex)
            {
                status = "fail";
                message = ex.Message;
            }

            return Json(new { status, message});
        }
    }
}
