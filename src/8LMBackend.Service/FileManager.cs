using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _8LMBackend.DataAccess.Enums;
using _8LMBackend.DataAccess.Models;
using Microsoft.AspNetCore.Http;

namespace _8LMBackend.Service
{
    public class FileManager: IFileManager
    {
        private readonly IPagesService _pagesService;
        public FileManager(IPagesService pagesService){
            _pagesService = pagesService;
        }

        /// <summary>
        /// Save file to the disk and db
        /// </summary>
        /// <param name="type">StorageType</param>
        /// <param name="token">Token</param>
        /// <param name="file">File to save</param>
        public Guid SaveFile(StorageType type, string token, IFormFile file)
        {
            try
            {
                int UserID = _pagesService.GetUserID(token);
                string dir = EnumExtensions.GetEnumDescription(type) + UserID.ToString();
                Guid newObjId = Guid.NewGuid();
                string cn = newObjId.ToString();
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (var stream = new FileStream(dir + "/" + cn, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                //save into new table
                //new guid, string originalname file
                return newObjId;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}