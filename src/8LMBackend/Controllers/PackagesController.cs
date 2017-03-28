using _8LMBackend.DataAccess.Models;
using _8LMBackend.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;

namespace _8LMCore.Controllers
{
    public class PackagesController : Controller
    {        
        private readonly ISubscribeService _subscribeService;

        public PackagesController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }
        public JsonResult SavePackage(Package package){
            if(package.Id == null){
                Package pack = new Package();
                pack.IsActual = null;
                _subscribeService.SavePackage(pack);
                return Json(new{status = "ok", id = pack.Id});
            }else{
                return Json(new{status = "fail", message = "Package is actual, so not saved"});
            }
        }

        public JsonResult DeletePackage(int id){
            try
            {
                _subscribeService.DeletePackage(id);
                return Json(new{status = "ok", message = "Package was deleted"});
            }
            catch(System.Exception ex)
            {
                return Json(new{status = "fail", message = ex.Message});
            }
        }

        public JsonResult UpdatePackage(Package package){            
            try
            {
                _subscribeService.UpdatePackage(package);
                return Json(new{status = "ok", message = "Package was updated"});
            }
            catch (System.Exception ex)
            {                
                return Json(new{status = "fail", message = ex.Message});
            }
        }

        public Service[] GetAllServices(){
            return _subscribeService.GetAllServices();
        }
    }
}
