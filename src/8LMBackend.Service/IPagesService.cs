using System.Collections.Generic;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Models;
using System;

namespace _8LMBackend.Service
{
    public interface IPagesService
    {
		int NewPage(Pages pages);
    }
}
