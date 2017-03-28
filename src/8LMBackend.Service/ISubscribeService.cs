using System.Collections.Generic;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Models;
using System;

namespace _8LMBackend.Service
{
    public interface ISubscribeService
    {
        void SavePackage(Package package);
        void DeletePackage(int id);
        void UpdatePackage(Package package);
        _8LMBackend.DataAccess.Models.Service[] GetAllServices();
    }
}
