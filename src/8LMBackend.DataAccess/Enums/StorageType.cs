using System;
using _8LMBackend.DataAccess;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using _8LMBackend.DataAccess.Enums;
using System.Linq;

namespace _8LMBackend.DataAccess.Enums
{
    public enum StorageType 
    {
        [Display(Description = "Gallery")]
        Gallery = 1,

        [Display(Description = "SupplierAssets")]
        SupplierAssets = 2
        
    }
}

public static class EnumExtensions
{
    public static string GetEnumDescription(this StorageType val)
    {
       DisplayAttribute attribute = val.GetType()
            .GetField(val.ToString())
            .GetCustomAttributes(typeof(DisplayAttribute ), false)
            .SingleOrDefault() as DisplayAttribute ;
        return attribute == null ? val.ToString() : attribute.Description;

    }
} 