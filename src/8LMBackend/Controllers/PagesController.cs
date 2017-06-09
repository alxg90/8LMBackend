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
        public JsonResult NewPage(string token)
        {
            try
            {
                return Json(new { status = "OK", PageID = _pagesService.NewPage(token) });
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

        public dtoPage[] GetPages(string token)
        {
            return _pagesService.GetPages(token).ToArray();
        }
    }
}
