using _8LMBackend.DataAccess.Models;
using System;
using _8LMBackend.DataAccess.Enums;
using Microsoft.AspNetCore.Http;

namespace _8LMBackend.Service
{
    public interface IFileManagerService
    {
        /// <summary>
        /// Save file to the disk and db
        /// </summary>
        /// <param name="type">StorageType</param>
        /// <param name="file">File to save</param>
        /// <param name="userId">UserID</param>
        int SaveFile(StorageType type, IFormFile file, int userId);

        /// <summary>
        /// Return file path
        /// </summary>
        /// <param name="type">StorageType</param>
        /// <param name="model">FileLibrary Model</param>
        /// <param name="userId">userId</param>
        string GetFilePath(StorageType type, FileLibrary model, int userId);

        /// <summary>
        /// Remove file
        /// </summary>
        /// <param name="type">StorageType</param>
        /// <param name="userId">UserID</param>
        /// <param name="logoId">Logo to remove</param>
        void RemoveFile(StorageType type, int userId, int logoId);
    }
}
