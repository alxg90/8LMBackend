using System;
using System.ComponentModel.DataAnnotations;
using _8LMBackend.DataAccess;
using _8LMBackend.DataAccess.Enums;

namespace _8LMBackend.DataAccess.Enums
{
    public enum SecurityFunctions
    {
        [Display(Description = "View list of campaigns")]
        Campaigns_RO = 1,

        [Display(Description = "CrUD campaigns")]
        Campaigns_CrUD = 2,

        [Display(Description = "View short statistics")]
        Campaigns_ShortStat_RO = 3,

        [Display(Description = "View distributors statistics")]
        Campaigns_DistStat_RO = 4,

        [Display(Description = "View reporting statistics")]
        Campaigns_ReportingStat_RO = 5,

        [Display(Description = "View list of Emails")]
        Emails_RO = 6,

        [Display(Description = "CrUD list of Emails")]
        Emails_CrUD = 7,

        [Display(Description = "CrUD Ads")]
        Emails_Ads_CrUD = 8,

        [Display(Description = "Set Broadcasts")]
        Emails_Broadcast = 9,

        [Display(Description = "View Landing Pages")]
        LandingPage_RO = 10,

        [Display(Description = "CrUD Landing Pages")]
        LandingPage_CrUD = 11,

        [Display(Description = "View ePages")]
        ePages_RO = 12,

        [Display(Description = "CrUD ePages")]
        ePages_CrUD = 13,

        [Display(Description = "CrUD customer covers")]
        ePages_Covers_CrUD = 14,

        [Display(Description = "View list of suppliers")]
        PromoEQP_RO = 15,

        [Display(Description = "CrUD suppliers")]
        PromoEQP_CrUD = 16,

        [Display(Description = "CrUD monthly codes")]
        PromoEQP_MonthlyCode_CrUD = 17,

        [Display(Description = "CrUD special promo codes for suppliers")]
        PromoEQP_CustomCodes_CrUD = 18,

        [Display(Description = "View list of Assets")]
        ManageAssets_RO = 19,

        [Display(Description = "CrUD Assets")]
        ManageAssets_CrUD = 20,

        [Display(Description = "CrUD Packages")]
        Packages_CrUD = 21,

        [Display(Description = "View list of users")]
        UserManagement_Roles_RO = 22,

        [Display(Description = "CrUD users and their Roles")]
        UserManagement_CrUD = 23,

        [Display(Description = "CrUD Roles")]
        UserManagement_Roles_CrUD = 24,

        [Display(Description = "Change password")]
        UserManagement_ChangePassword = 25,

        [Display(Description = "View list of services")]
        Services_RO = 26,

        [Display(Description = "CrUD services and their permissions")]
        Services_CrUD = 27,

        [Display(Description = "BRND access")]
        BRND = 28,

        [Display(Description = "500 participants")]
        Email500 = 29,

        [Display(Description = "2500 participants")]
        Email2500 = 30,

        [Display(Description = "5000 participants")]
        Email5000 = 31,

        [Display(Description = "10000 participants")]
        Email10000 = 32

    }
}