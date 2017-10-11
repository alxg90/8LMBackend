using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _8LMBackend.DataAccess.Enums;
using _8LMBackend.DataAccess.Infrastructure;
using _8LMBackend.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace _8LMBackend.Service
{
    public class FileManagerService: ServiceBase, IFileManagerService
    {
        private readonly IPagesService _pagesService;
        public const string rootFolder = "Content/";
        public FileManagerService(IDbFactory dbFactory, IPagesService pagesService)
            : base(dbFactory)
        {
            _pagesService = pagesService;
        }

        /// <summary>
        /// Save file to the disk and db
        /// </summary>
        /// <param name="type">StorageType</param>
        /// <param name="file">File to save</param>
        /// <param name="userId">UserID</param>
        public int SaveFile(StorageType type, IFormFile file, int userId)
        {
            try
            {
                var extension = System.IO.Path.GetExtension(file.FileName);
                string folder = rootFolder + EnumExtensions.GetEnumDescription(type) + userId.ToString();
                string newObjId = Guid.NewGuid().ToString();
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                using (var stream = new FileStream(folder + "/" + newObjId + extension, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                //save into the new table
                FileLibrary fileLibrary = new FileLibrary{
                    CurrentName = newObjId + extension,
                    FileName = file.FileName,
                };

                DbContext.Add(fileLibrary);
                DbContext.SaveChanges();

                return fileLibrary.ID;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Get File path
        /// </summary>
        /// <param name="type">StorageType</param>
        /// <param name="model">Model</param>
        /// <param name="userId">UserID</param>
        public string GetFilePath(StorageType type, FileLibrary model, int userId)
        {
            var filePath = string.Empty;
            string dir = rootFolder + EnumExtensions.GetEnumDescription(type) + userId.ToString();
            filePath =  dir + "/" + model.CurrentName;     
            
            return filePath;
            
        }

        /// <summary>
        /// Remove file 
        /// </summary>
        /// <param name="type">StorageType</param>
        /// <param name="userId">UserId</param>
        /// <param name="logoId">Logo to remove</param>
        public void RemoveFile(StorageType type, int userId, int logoId)
        {
            try
            {
                var currentLogo = DbContext.FileLibrary.FirstOrDefault(x=> x.ID == logoId);
                if (currentLogo != null){
                    DbContext.Entry(currentLogo).State = EntityState.Deleted;
                }
                string folder = rootFolder + EnumExtensions.GetEnumDescription(type) + userId.ToString();
                if (Directory.Exists(folder))
                {
                    var filepath = folder + "/" + currentLogo.CurrentName;
                    File.Delete(filepath);
                }

                DbContext.SaveChanges();
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}