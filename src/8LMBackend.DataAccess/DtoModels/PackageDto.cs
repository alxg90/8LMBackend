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
        public ServicesDto[] Services { get; set; }
        public int PaletteId { get; set; }
        public int? StatusId { get; set; }
        public List<PackageRatePlanDto> PackageRatePlans { get; set; }        
        public DateTime ValidTo { get; set; }
    }
    public class PackageRatePlanDto{
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int DurationInMonths { get; set; }
        public int Price { get; set; } 
        public int CurrencyId { get; set; }
        public bool Bought { get; set; }
        public List<PackageReferenceCodeDto> PackageReferenceCode { get; set; }
        public List<PackageReferenceExtendCodeDto> PackageReferenceExtendCode { get; set; }
        public List<PackageReferenceServiceCodeDto>  PackageReferenceServiceCode { get; set; }
    }
    public class ServicesDto{
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PackageReferenceCodeDto
    {
        public int PackRateId { get; set; }
        public string ReferenceCode { get; set; }
        public bool IsFixed { get; set; }
        public int Value { get; set; }
    }
    public class PackageReferenceServiceCodeDto
    {
        public int PackRateId { get; set; }
        public string ReferenceCode { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
    }
    public partial class PackageReferenceExtendCodeDto
    {
        public int PackRateId { get; set; }
        public string ReferenceCode { get; set; }
        public int Months { get; set; }
    }
}
