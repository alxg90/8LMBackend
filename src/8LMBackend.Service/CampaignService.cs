using System.Collections.Generic;
using _8LMBackend.DataAccess.Models;
using _8LMBackend.DataAccess.Repositories;
using _8LMBackend.DataAccess.Infrastructure;
using System.Linq;

namespace _8LMBackend.Service
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CampaignService(ICampaignsRepository campaignsRepository, IUnitOfWork unitOfWork)
        {
            _campaignsRepository = campaignsRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateCampaign(Campaign campaign)
        {
            _campaignsRepository.Add(campaign);
        }

        public Campaign GetCampaign(string name)
        {
            var campaign = _campaignsRepository.GetCampaignByName(name);
            return campaign;
        }

        public Campaign GetCampaign(int id)
        {
            var campaign = _campaignsRepository.GetById(id);
            return campaign;
        }

        public IEnumerable<Campaign> GetCampaigns(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return _campaignsRepository.GetAll();
            else
                return _campaignsRepository.GetAll().Where(c => c.Name == name);
        }

        public void SaveCampaign()
        {
            _unitOfWork.SaveChanges();
        }
    }
}
