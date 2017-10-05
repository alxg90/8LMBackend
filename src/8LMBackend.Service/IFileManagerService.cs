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
        /// <param name="token">Token</param>
        /// <param name="file">File to save</param>
        string SaveFile(StorageType type, string token, IFormFile file);
    }
}
