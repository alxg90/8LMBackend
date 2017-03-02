using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Promoproduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
    }
}
