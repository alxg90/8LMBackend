using System;
using System.Collections.Generic;

namespace _8LMBackend.DataAccess.Models
{
    public partial class Campaignstatus
    {
        public Campaignstatus()
        {
            Campaign = new HashSet<Campaign>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Campaign> Campaign { get; set; }
    }
}
