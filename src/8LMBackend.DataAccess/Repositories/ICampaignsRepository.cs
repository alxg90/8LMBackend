using _8LMBackend.DataAccess.Infrastructure;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.DataAccess.Repositories
{
    public interface ICampaignsRepository : IRepository<Campaign>
    {
        Campaign GetCampaignByName(string campaignName);
    }
}
