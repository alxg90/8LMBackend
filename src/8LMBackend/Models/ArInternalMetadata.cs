using System;
using System.Collections.Generic;

namespace _8LMCore.Models
{
    public partial class ArInternalMetadata
    {
        public string Key { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Value { get; set; }
    }
}
