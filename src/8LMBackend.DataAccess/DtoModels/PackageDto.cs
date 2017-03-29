using System;
using System.Collections.Generic;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.DataAccess.DtoModels
{
    public partial class PackageDto
    {
        public PackageDto()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] Services { get; set; }
        public int PaletteId { get; set; }
        public int Duration { get; set; }
        public int Price { get; set; }
        public int Currency { get; set; }
        public PackageReferenceCodeDto PackageReferenceCode { get; set; }
        public PackageReferenceExtendCode PackageReferenceExtendCode { get; set; }
        public PackageReferenceServiceCode PackageReferenceServiceCode { get; set; }
    }

    public class PackageReferenceCodeDto
    {
        public int PackageId { get; set; }
        public string ReferenceCode { get; set; }
        public bool IsFixed { get; set; }
        public int Value { get; set; }

    }
}
