using System;
using System.Collections.Generic;
using System.Linq;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using _8LMBackend.DataAccess.Models;
using System.IO;
using _8LMBackend.DataAccess.DtoModels;
using _8LMBackend.Service.DTO;

namespace _8LMBackend.Service
{
    public class PagesService : ServiceBase, IPagesService
    {
		public PagesService(IDbFactory dbFactory)
			: base(dbFactory) 
		{
		}

        public int NewLandingPage(string token)
        {
            return NewPage(token, Types.Pages.LandingPage);
        }

        public int NewEmailPage(string token)
        {
            return NewPage(token, Types.Pages.Email);
        }

        int NewPage(string token, int TypeID)
        {
            Pages page = new Pages()
            {
                Name = Guid.NewGuid().ToString(),
                Json = string.Empty,
                Html = string.Empty,
                TypeId = TypeID,
                StatusId = Statuses.Pages.NotInitialized,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = GetUserID(token)
            };

            DbContext.Pages.Add(page);
            DbContext.SaveChanges();
            
            return page.Id;
        }

        public Pages GetPage(int id)
        {
            return DbContext.Pages.Where(p => p.Id == id).FirstOrDefault();
        }

        public MemoryStream Download(Pages page)
        {
            PageSpider ps = new PageSpider(page.Html);
            return ps.Load();
        }

        List<int> GetFunctionsForUser(string access_token)
        {
            var userId = GetUserID(access_token);
            if (DbContext.Users.FirstOrDefault(x => x.Id == userId).TypeId == Types.Users.Admin)
            {
                return DbContext.SecurityFunction.Select(sf => sf.Id).ToList();
            }
            var result = DbContext.UserRole.Where(p => p.UserId == userId)
                .Include(p => p.Role)
                .Join(DbContext.RoleFunction, p => p.RoleId, rf => rf.RoleId, (p, rf) => rf.FunctionId)
                .Distinct().ToList();

            var res = DbContext.Subscription.Include("PackageRatePlan")
                        .Where(p => p.UserId == userId
                            && p.StatusId == Statuses.Subscription.Active
                            && p.EffectiveDate.Date <= DateTime.UtcNow.Date
                            && p.ExpirationDate.Date >= DateTime.UtcNow.Date)
                        .Join(DbContext.PackageService, s => s.PackageRatePlan.PackageId, ps => ps.PackageId, (s, ps) => ps)
                        .Join(DbContext.ServiceFunction, ps => ps.ServiceId, sf => sf.ServiceId, (ps, sf) => sf)
                        .Select(sf => sf.SecurityFunctionId).ToList();

            result.AddRange(res);

            return result.Distinct().ToList();
        }

        int GetUserID(string access_token)
        {
            var u = DbContext.UserToken.Where(p => p.Token == access_token).FirstOrDefault();
            if (u == default(UserToken))
                throw new Exception("Not authorized");

            return u.UserId;
        }

        public void UpdatePageMeta(dtoPage page, string token)
        {
            var functions = GetFunctionsForUser(token);
            //TODO: check functions

            DbContext.RemoveRange(DbContext.PageTag.Where(p => p.PageId == page.ID).ToList());
            var item = DbContext.Pages.Where(p => p.Id == page.ID).FirstOrDefault();
            if (item == null)
                throw new Exception("Page with ID = " + page.ID.ToString() + " not found");

            item.Name = page.Name;
            item.Description = page.Description;
            //item.Json = page.JSON;
            //item.Html = page.HTML;
            item.StatusId = Statuses.Pages.Active;

            foreach (var t in page.tags)
            {
                var tag = DbContext.Tags.Where(p => p.Tag == t.Tag).FirstOrDefault();
                if (tag == null)
                {
                    var nt = new Tags()
                    {
                        Tag = t.Tag
                    };
                    DbContext.Add(nt);
                    tag.Id = nt.Id;
                }

                var npt = new PageTag()
                {
                    PageId = page.ID,
                    TagId = tag.Id
                };
                DbContext.Add(npt);
            }

            DbContext.SaveChanges();
        }

        public void UpdatePage(dtoPage page, string token)
        {
            var functions = GetFunctionsForUser(token);
            //TODO: check functions
            
            var item = DbContext.Pages.Where(p => p.Id == page.ID).FirstOrDefault();
            if (item == null)
                throw new Exception("Page with ID = " + page.ID.ToString() + " not found");

            item.Name = page.Name;
            //item.Description = page.Description;
            item.Json = page.JSON;
            item.Html = page.HTML;
            item.StatusId = Statuses.Pages.Active;

            DbContext.SaveChanges();
        }

        public void DeletePage(dtoPage page, string token)
        {
            var functions = GetFunctionsForUser(token);
            //TODO: check functions
            var item = DbContext.Pages.Where(p => p.Id == page.ID).FirstOrDefault();
            if (item == null)
                throw new Exception("Page with ID = " + page.ID.ToString() + " not found");

            DbContext.Remove(item);
            DbContext.SaveChanges();
        }

        public List<dtoPage> GetLandingPages(string token)
        {
            return GetPages(token, Types.Pages.LandingPage);
        }

        public List<dtoPage> GetEmailPages(string token)
        {
            return GetPages(token, Types.Pages.Email);
        }

        List<dtoPage> GetPages(string token, int TypeID)
        {
            var functions = GetFunctionsForUser(token);
            //TODO: check functions

            List<dtoPage> result = new List<dtoPage>();
            var pages = DbContext.Pages.Where(p => p.CreatedBy == GetUserID(token) && p.StatusId == Statuses.Pages.Active && p.TypeId == TypeID).ToList();
            foreach (var item in pages)
            {
                var p = new dtoPage()
                {
                    ID = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    TypeID = item.TypeId,
                    //JSON = item.Json,
                    //HTML = item.Html,
                    tags = new List<dtoPageTag>()
                };

                var ts = DbContext.PageTag.Where(t => t.PageId == item.Id).Include(tp => tp.Tag).ToList();
                foreach (var it in ts)
                    p.tags.Add(new dtoPageTag() { ID = it.TagId, Tag = it.Tag.Tag });

                result.Add(p);
            }

            return result;
        }

        void Activate(dtoPage page, string token, bool active)
        {
            var functions = GetFunctionsForUser(token);
            //TODO: check functions
            DbContext.RemoveRange(DbContext.PageTag.Where(p => p.PageId == page.ID).ToList());
            var item = DbContext.Pages.Where(p => p.Id == page.ID).FirstOrDefault();
            if (item == null)
                throw new Exception("Page with ID = " + page.ID.ToString() + " not found");

            item.StatusId = active ? Statuses.Pages.Active : Statuses.Pages.Inactive;

            DbContext.SaveChanges();
        }

        public void Activate(dtoPage page, string token)
        {
            Activate(page, token, true);
        }

        public void Deactivate(dtoPage page, string token)
        {
            Activate(page, token, false);
        }

        public string HTML(int id)
        {
            var item = DbContext.Pages.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
                throw new Exception("Page with ID = " + id.ToString() + " not found");

            return item.Html;
        }
    }
}
