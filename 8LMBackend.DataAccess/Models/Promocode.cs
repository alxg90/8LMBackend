using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Promocode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int FromDate { get; set; }
        public int ToDate { get; set; }
    }
}
