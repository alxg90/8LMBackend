using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8LMCore.Controllers
{
    public class Extention
    {
        Dictionary<string, string> ext = new Dictionary<string, string>();

        public Extention()
        {
            ext.Add(".bmp", "image/bmp");
            ext.Add(".cgm", "image/cgm");
            ext.Add(".djv", "image/vnd.djvu");
            ext.Add(".djvu", "image/vnd.djvu");
            ext.Add(".gif", "image/gif");
            ext.Add(".ico", "image/x-icon");
            ext.Add(".ief", "image/ief");
            ext.Add(".jp2", "image/jp2");
            ext.Add(".jpe", "image/jpeg");
            ext.Add(".jpeg", "image/jpeg");
            ext.Add(".jpg", "image/jpeg");
            ext.Add(".mac", "image/x-macpaint");
            ext.Add(".pbm", "image/x-portable-bitmap");
            ext.Add(".pct", "image/pict");
            ext.Add(".pic", "image/pict");
            ext.Add(".pict", "image/pict");
            ext.Add(".png", "image/png");
            ext.Add(".pnm", "image/x-portable-anymap");
            ext.Add(".pnt", "image/x-macpaint");
            ext.Add(".pntg", "image/x-macpaint");
            ext.Add(".ppm", "image/x-portable-pixmap");
            ext.Add(".qti", "image/x-quicktime");
            ext.Add(".qtif", "image/x-quicktime");
            ext.Add(".ras", "image/x-cmu-raster");
            ext.Add(".rgb", "image/x-rgb");
            ext.Add(".svg", "image/svg+xml");
            ext.Add(".tif", "image/tiff");
            ext.Add(".tiff", "image/tiff");
            ext.Add(".wbmp", "image/vnd.wap.wbmp");
            ext.Add(".xbm", "image/x-xbitmap");
            ext.Add(".xpm", "image/x-xpixmap");
            ext.Add(".xwd", "image/x-xwindowdump");
        }

        public string Get(string e)
        {
            string result;
            if (!ext.TryGetValue(e.ToLower(), out result))
                throw new Exception("File extention not resolved");

            return result;
        }
    }
}
