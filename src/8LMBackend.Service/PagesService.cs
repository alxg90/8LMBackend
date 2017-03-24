using System;
using System.Collections.Generic;
using System.Linq;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using _8LMBackend.DataAccess.Models;

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
    }
}
