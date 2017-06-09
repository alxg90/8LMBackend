using System.Collections.Generic;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Models;
using System;
using System.IO;

namespace _8LMBackend.Service
{
    public interface IPagesService
    {
		int NewPage(Pages page);
        Pages GetPage(int id);
        MemoryStream Download(Pages page);
    }
}
