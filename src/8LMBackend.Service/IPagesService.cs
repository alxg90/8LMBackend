using System.Collections.Generic;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Models;
using System;
using System.IO;
using _8LMBackend.Service.DTO;
using _8LMBackend.DataAccess.Enums;

namespace _8LMBackend.Service
{
    public interface IPagesService
    {
        int NewLandingPage(string token);
        int NewEmailPage(string token);
        Pages GetPage(int id);
        PageControl GetPageControl(int id);
        MemoryStream Download(Pages page);
        void UpdatePage(dtoPage page, string token);
        void UpdatePageMeta(dtoPage page, string token);
        void DeletePage(dtoPage page, string token);
        List<dtoPage> GetLandingPages(string token);
        List<dtoPage> GetControls(int ParentID, string token);
        List<dtoPage> GetThemes(string token, int ThemeType);
        List<dtoPage> GetEmailPages(string token);
        void Activate(dtoPage page, string token);
        void Deactivate(dtoPage page, string token);
        string HTML(int id);
        int GetDefaultLandingPageTemplateID();
        int GetDefaultEmailTemplateID();
        int GetUserID(string access_token);
        void SaveGallery(Gallery image, string token);
        GalleryViewModel GetGalleryList(GalleryType type, int PageCapacity, int PageNumber, string search, string token);
        Gallery GetGallery(int ID, string token);
        void RemoveGalleryItem(int ID, string token);
        void UpdateGalleryItem(int ID, string Name, string token);
    }
}
