using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class UserTokenStatus
    {
        public UserTokenStatus()
        {
            UserToken = new HashSet<UserToken>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserToken> UserToken { get; set; }
    }
}
