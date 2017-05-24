using System.IO;
using System;
using Microsoft.Extensions.PlatformAbstractions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
public class FileManager
{    
    public bool writeFiles(List<Microsoft.AspNetCore.Http.IFormFile> files)
    {
        try
        {
            var path = Directory.GetCurrentDirectory();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = file.OpenReadStream())
                    using (var ms = new MemoryStream())
                    {
                        fileStream.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        File.WriteAllBytes(path + "/" + file.FileName, fileBytes);
                    }
                }
            }
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}