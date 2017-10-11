using System;
using _8LMBackend.DataAccess;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using _8LMBackend.DataAccess.Enums;

namespace _8LMBackend.DataAccess.Enums
{
    public enum UploadedFileState 
    {
        Unknown,
        Create,
        Update,
        Delete
    }
}