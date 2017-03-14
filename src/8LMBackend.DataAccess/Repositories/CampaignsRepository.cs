using _8LMBackend.DataAccess.Infrastructure;
using _8LMBackend.DataAccess.Models;
using System.Linq;

namespace _8LMBackend.DataAccess.Repositories
{
    public class CampaignsRepository : RepositoryBase<Campaign>, ICampaignsRepository
    {
        public CampaignsRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Campaign GetCampaignByName(string campaignName)
        {
            var category = DbContext.Campaign.Where(c => c.Name == campaignName).FirstOrDefault();
            return category;
        }
    }
}
