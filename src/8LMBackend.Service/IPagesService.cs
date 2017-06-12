using System.Collections.Generic;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Models;
using System;
using System.IO;
using _8LMBackend.Service.DTO;

namespace _8LMBackend.Service
{
    public interface IPagesService
    {
        int NewLandingPage(string token);
        int NewEmailPage(string token);
        Pages GetPage(int id);
        MemoryStream Download(Pages page);
        void UpdatePage(dtoPage page, string token);
        void UpdatePageMeta(dtoPage page, string token);
        void DeletePage(dtoPage page, string token);
        List<dtoPage> GetLandingPages(string token);
        List<dtoPage> GetEmailPages(string token);
        void Activate(dtoPage page, string token);
        void Deactivate(dtoPage page, string token);
    }
}
