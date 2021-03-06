﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _8LMBackend.DataAccess.Infrastructure;

namespace _8LMBackend.Service.ViewModels
{
    public class AccountViewModel
    {
		public int id { get; set; }
		public string login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string ClearPassword { get; set; }
        public string Email { get; set; }
        public string Icon { get; set; }
        public string company { get; set; }
        public string phone { get; set; }
        public string mailing_state { get; set; }
        public int typeID { get; set; }
		public string typeName { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string mailing_address { get; set; }
        public string mailing_address2 { get; set; }
        public string mailing_city { get; set; }
        public string mailing_zip { get; set; }
        public string mailing_country { get; set; }

        public List<RoleViewModel> roles { get; set; }
        public List<PackageViewModel> packages { get; set; }
    }

    public class PackageViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int  NumberOfMonths { get; set; }
        public int Price { get; set; }
        public DateTime? term { get; set; }
        public DateTime? NextBillingDate { get; set; }
        public DateTime BoughtDate { get; set; }
        public string Status { get; set; }
        public List<ServiceViewModel> Services { get; set; }
    }

    public class ServiceViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
