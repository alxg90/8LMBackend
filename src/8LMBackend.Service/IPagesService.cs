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
        int NewPage(string token);
        Pages GetPage(int id);
        MemoryStream Download(Pages page);
        void UpdatePage(dtoPage page, string token);
        void DeletePage(dtoPage page, string token);
        List<dtoPage> GetPages(string token);
    }
}
