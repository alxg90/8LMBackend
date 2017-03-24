using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class UserContact
    {
        public int UserId { get; set; }
        public int ContactTypeId { get; set; }
        public string Value { get; set; }

        public virtual ContactType ContactType { get; set; }
        public virtual Users User { get; set; }
    }
}
