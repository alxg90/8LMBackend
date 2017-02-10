using _8LMBackend.DataAccess.Models;
using System.Collections.Generic;

namespace _8LMBackend.Service
{
    public interface ICampaignService
    {
        IEnumerable<Campaign> GetCampaigns(string name = null);
        Campaign GetCampaign(int id);
        Campaign GetCampaign(string name);
        void CreateCampaign(Campaign campaign);
        void SaveCampaign();
    }
}
