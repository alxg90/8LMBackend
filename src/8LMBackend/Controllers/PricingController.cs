using _8LMBackend.DataAccess.Models;
using _8LMBackend.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

using System.IO;
using System.Text;

namespace _8LMCore.Controllers
{
    public class PricingController : Controller
    {
        [HttpPost]
        public ActionResult ThankYouPage(){
            var log = System.IO.File.CreateText("log.txt");
            var bldr = new StringBuilder();
            foreach(var key in Request.Form.Keys){
                var val = Request.Form[key];
                bldr.Append(key+ "=" + val);
            }
            log.Write(bldr.ToString());
            log.Flush();
            return Json(new {status = "ok"});
        }

        public ActionResult Buy(){
            return View();
        }

    }

    
}
