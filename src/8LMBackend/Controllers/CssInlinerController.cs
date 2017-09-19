using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CssInliner.Controllers
{
    public class CssInlinerController : Controller
    {
        [HttpPost]
        public string Convert(ConvertRequestParam convertParam)
        {
            if (convertParam.Key != "65214651465")
            {
                return "Game over!";
            }

            using (var pm = new PreMailer.Net.PreMailer(convertParam.Html))
            {
                var document = pm.Document;
                var result = pm.MoveCssInline();
                return result.Html;
            }
        }

        public class ConvertRequestParam
        {
            public string Html { get; set; }
            public string Key { get; set; }
        }
    }
}
