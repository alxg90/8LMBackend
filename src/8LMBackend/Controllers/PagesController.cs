using _8LMBackend.DataAccess.Models;
using _8LMBackend.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace _8LMCore.Controllers
{
    public class PagesController : Controller
    {
        private readonly IPagesService _pagesService;

        public PagesController(IPagesService pagesService)
        {
            _pagesService = pagesService;
        }
        
        public int NewPage(){
            var page = new Pages();
            page.Html = "Empty Page";
            page.Json = "Empty Page";
            page.Name = "Empty Page" + DateTime.Now;
            page.TypeId = 1;
            page.StatusId = 1;
            page.CreatedDate = DateTime.Now;
            page.CreatedBy = 1;
            _pagesService.NewPage(page);
            return page.Id;
        }
    }
}
