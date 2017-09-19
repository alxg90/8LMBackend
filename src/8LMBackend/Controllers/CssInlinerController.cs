using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace _8LMCore.Controllers
{
    public class CssInlinerController : Controller
    {
        [HttpPost]
        public ActionResult Convert([FromBody]ConvertRequestParam convertParam, string token)
        {
            if (convertParam.Key != "65214651465")
            {
                return Json( new {error = "Game over!"});
            }

            using (var pm = new PreMailer.Net.PreMailer(convertParam.Html))
            {
                var document = pm.Document;
                var result = pm.MoveCssInline();
                return Json(new {result = result.Html});
            }
        }

        public class ConvertRequestParam
        {
            public string Html { get; set; }
            public string Key { get; set; }
        }
    }
}
