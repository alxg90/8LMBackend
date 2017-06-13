using _8LMBackend.DataAccess.Models;
using _8LMBackend.DataAccess.DtoModels;
using _8LMBackend.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.IO;
using _8LMBackend.Service.DTO;

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
                ViewBag.html = _pagesService.HTML(id);
                return View();
            }
            catch (Exception ex)
            {
                return ViewBag.html = ex.Message;
            }
        }
    }
}
