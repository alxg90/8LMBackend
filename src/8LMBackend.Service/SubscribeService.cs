using System;
using System.Collections.Generic;
using System.Linq;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.Service
{
    public class SubscribeService : ServiceBase, ISubscribeService
    {
		public SubscribeService(IDbFactory dbFactory)
			: base(dbFactory) 
		{
		}
        public void SavePackage(Package package){
            DbContext.Package.Add(package);
            DbContext.SaveChanges();
        }
        public void DeletePackage(int id){
            var package = DbContext.Package.FirstOrDefault(x => x.Id == id);
            if(package.IsActual==null){
                DbContext.Package.Remove(package);
                DbContext.SaveChanges();
            }
        }
        public void UpdatePackage(Package package){
            DbContext.Package.Update(package);
            DbContext.SaveChanges();
        }

        public _8LMBackend.DataAccess.Models.Service[] GetAllServices(){
            return DbContext.Service.ToArray();
        } 
    }
}
