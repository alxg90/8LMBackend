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
        public string VideoURL { get; set; }
        public int SortOrder { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
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
        public string Logo { get; set; }
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

    public class PackageDashboard
    {
        public string MonthlyCode { get; set; }
        public int NumberOfSuppliers { get; set; }
        public bool MorePackagesAvailable { get; set; }
        public List<PackageDto> packages { get; set; }
    }

    public class AvailableEmailBroadcast
    {
        public int Amount { get; set; }
        public int Participants { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
    }

    public class UpgradeSubscriptionResponse
    {
        public int RequiredRatePlanID { get; set; }
        public int EmailLimitBroadcast { get; set; }
        public int EmailLimitAddress { get; set; }
        public int Price { get; set; }
        public int AmountDue { get; set; }
        public int CurrentRatePlanID { get; set; }
        public int CurrentEmailLimitBroadcast { get; set; }
        public int CurrentEmailLimitAddress { get; set; }
        public int DurationInMonth { get; set; }
    }

    public class UpgradePackageRequest
    {
        public int CurrentRatePlanID { get; set; }
        public int NewRatePlanID { get; set; }
        public Guid UpgradeRequestID { get; set; }
        public int listID { get; set; }
    }
}
