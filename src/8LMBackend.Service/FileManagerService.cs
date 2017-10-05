using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _8LMBackend.DataAccess.Enums;
using _8LMBackend.DataAccess.Infrastructure;
using _8LMBackend.DataAccess.Models;
using Microsoft.AspNetCore.Http;

namespace _8LMBackend.Service
{
    public class FileManagerService: ServiceBase, IFileManagerService
    {
        private readonly IPagesService _pagesService;
        public FileManagerService(IDbFactory dbFactory, IPagesService pagesService)
            : base(dbFactory)
        {
            _pagesService = pagesService;
        }

        /// <summary>
        /// Save file to the disk and db
        /// </summary>
        /// <param name="type">StorageType</param>
        /// <param name="token">Token</param>
        /// <param name="file">File to save</param>
        public string SaveFile(StorageType type, string token, IFormFile file)
        {
            try
            {
                int userId = _pagesService.GetUserID(token);
                string dir = EnumExtensions.GetEnumDescription(type) + userId.ToString();
                string newObjId = Guid.NewGuid().ToString();
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (var stream = new FileStream(dir + "/" + newObjId, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                //save into new table
                FileLibrary fileLibrary = new FileLibrary{
                    CurrentName = newObjId,
                    FileName = file.FileName,
                };

                DbContext.Add(fileLibrary);
                DbContext.SaveChanges();

                return newObjId;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}