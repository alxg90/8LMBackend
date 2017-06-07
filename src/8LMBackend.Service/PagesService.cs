using System;
using System.Collections.Generic;
using System.Linq;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using _8LMBackend.DataAccess.Models;
using System.IO;

namespace _8LMBackend.Service
{
    public class PagesService : ServiceBase, IPagesService
    {
		public PagesService(IDbFactory dbFactory)
			: base(dbFactory) 
		{
		}
        public int NewPage(Pages pages){
            DbContext.Pages.AddRange(pages);
            DbContext.SaveChanges();
            
            return pages.Id;
        }

        public Pages GetPage(int PageID)
        {
            return DbContext.Pages.Where(p => p.Id == PageID).FirstOrDefault();
        }

        public MemoryStream Download(Pages page)
        {
            PageSpider ps = new PageSpider(page.Html);
            return ps.Load();
        }
    }
}
