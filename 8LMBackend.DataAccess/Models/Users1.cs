using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Users1
    {
        public int Id { get; set; }
        public string ClearPass { get; set; }
        public DateTime CreatedAt { get; set; }
        public string HashedPassword { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Salt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
