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
                PreviewUrl = string.Empty,
                TypeId = TypeID,
                StatusId = Statuses.Pages.NotInitialized,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = GetUserID(token),
                IsPublic = false
            };

            DbContext.Pages.Add(page);
            DbContext.SaveChanges();
            
            return page.Id;
        }

        public Pages GetPage(int id)
        {
            var item = DbContext.Pages.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
                throw new Exception("Page with ID = " + id.ToString() + " not found");

            return item;
        }

        public PageControl GetPageControl(int id)
        {
            var item = DbContext.PageControl.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
                throw new Exception("PageControl with ID = " + id.ToString() + " not found");

            return item;
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

        public int GetUserID(string access_token)
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

            VerifyName(page.Name, page.ID, item.CreatedBy);

            item.Name = page.Name;
            item.Description = page.Description;
            item.StatusId = Statuses.Pages.Active;

            if (page.tags != null)
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

            VerifyName(page.Name, page.ID, item.CreatedBy);

            item.Name = page.Name;
            //item.Description = page.Description;
            item.Json = page.JSON;
            item.Html = page.HTML;
            item.PreviewUrl = page.PreviewUrl;
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

            //DbContext.Remove(item);
            item.StatusId = Statuses.Pages.Inactive;
            DbContext.SaveChanges();
        }

        public List<dtoPage> GetLandingPages(string token)
        {
            return GetPages(token, Types.Pages.LandingPage);
        }

        public List<dtoPage> GetControls(int ParentID, string token)
        {
            List<dtoPage> result = new List<dtoPage>();
            var pages = DbContext.PageControl.Where(p => p.ParentID == ParentID).ToList();
            foreach (var item in pages)
            {
                var p = new dtoPage()
                {
                    ID = item.Id,
                    Name = item.Name,
                    TypeID = item.TypeId,
                    JSON = item.Json,
                    //HTML = item.Html,
                    PreviewUrl = item.PreviewUrl,
                    tags = new List<dtoPageTag>()
                };

                result.Add(p);
            }

            return result;
        }

        public List<dtoPage> GetThemes(string token, int ThemeType) {
            List<dtoPage> result = new List<dtoPage>();
            var pages = DbContext.PageControl.Where(p => p.TypeId == ThemeType).ToList();
            foreach (var item in pages)
            {
                var p = new dtoPage()
                {
                    ID = item.Id,
                    Name = item.Name,
                    TypeID = item.TypeId,
                    PreviewUrl = item.PreviewUrl
                };

                result.Add(p);
            }

            return result;
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
            var pages = DbContext.Pages.Where(p => p.CreatedBy == GetUserID(token) && p.StatusId == Statuses.Pages.Active && p.TypeId == TypeID).OrderByDescending(p => p.CreatedDate).ToList();
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
                    PreviewUrl = item.PreviewUrl,
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

        void VerifyName(string name, int PageID, int UserID)
        {
            var result = DbContext.Pages.FirstOrDefault(p => p.Name.ToUpper() == name.ToUpper() && p.Id != PageID && p.CreatedBy == UserID);
            if (result != null)
                throw new Exception("This name has already been taken. Please select another one");
        }

        public int GetDefaultLandingPageTemplateID()
        {
            var result = DbContext.PaymentSetting.First();
            return result.defaultLandingPageTemplateID;
        }

        public int GetDefaultEmailTemplateID()
        {
            var result = DbContext.PaymentSetting.First();
            return result.defaultEmailTemplateID;
        }

        public void SaveGallery(Gallery image, string token)
        {
            DbContext.Gallery.Add(image);
            DbContext.SaveChanges();
        }

        public GalleryViewModel GetGalleryList(int PageCapacity, int PageNumber, string search, string token)
        {
            int UID = GetUserID(token);

            var logoQuery = PrepareGetGalleryQuery(1, PageCapacity, PageNumber, search, UID);
            var imageQuery = PrepareGetGalleryQuery(0, PageCapacity, PageNumber, search, UID);
            
            var totalPagesLogo = logoQuery.Count() / PageCapacity;
            var totalPagesImage = imageQuery.Count() / PageCapacity;

            return new GalleryViewModel {
                TotalPages = Math.Max(totalPagesImage, totalPagesLogo),
                Logos = logoQuery.Skip(PageNumber * PageCapacity).Take(PageCapacity).ToList(),
                Images = imageQuery.Skip(PageNumber * PageCapacity).Take(PageCapacity).ToList()
            };
        }

        private IQueryable<Gallery> PrepareGetGalleryQuery(int TypeID, int PageCapacity, int PageNumber, string search, int UID){
           
            var resultQuery = DbContext.Gallery.Where(p => p.UserID == UID && p.TypeID == TypeID);

            Func<IQueryable<Gallery>, IQueryable<Gallery>> filterImageExpr = (q) => 
            {
                return q.Where(x => x.Title.ToUpper().Contains(search.ToUpper()));
            };
            
            Func<IQueryable<Gallery>, IQueryable<Gallery>> filterLogoExpr = (q) => 
            {
                return q.Where(x => x.FileName.ToUpper().Contains(search.ToUpper()));
            };

            if (search != null && search.Length > 0){
                resultQuery = TypeID == 0 ? filterImageExpr(resultQuery) : filterLogoExpr(resultQuery);
            }

            return resultQuery;
        }

        public Gallery GetGallery(int ID, string token)
        {
            int UID = GetUserID(token);

            var item = DbContext.Gallery.FirstOrDefault(p => p.ID == ID);
            if (item == null)
                throw new Exception("Image with ID = " + ID.ToString() + " not found in Gallery");

            if (item.UserID != UID)
                throw new Exception("This image does not belong to the user");

            return item;
        }

        public void RemoveGalleryItem(int ID, string token)
        {
            var item = DbContext.Gallery.FirstOrDefault(p => p.ID == ID);

            if (item != null)
            {
                DbContext.Remove(item);
                DbContext.SaveChanges();
            }
        }

        public void UpdateGalleryItem(int ID, string Title, string token)
        {
            var item = DbContext.Gallery.FirstOrDefault(p => p.ID == ID);

            if (item != null)
            {
                item.Title = Title;
                DbContext.SaveChanges();
            }
            else
                throw new Exception("Gallery item with ID = " + ID.ToString() + " not found");
        }
    }
}
