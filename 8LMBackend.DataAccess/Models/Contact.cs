using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public int ContactTypeId { get; set; }
        public int UserTypeId { get; set; }
        public string Value { get; set; }
    }
}
