using _8LMBackend.DataAccess.Models;
using _8LMBackend.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace _8LMCore.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly ICampaignService _campaignService;

        public CampaignsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        public JsonResult GetCampaigns()
        {
            var campaigns = _campaignService.GetCampaigns();
            return Json(new { campaigns });
        }

        public JsonResult CreateCampaign()
        {
            var newCampaign = new Campaign();
            newCampaign.Name = "New Campaign 1";
            newCampaign.Description = "Desc";
            newCampaign.CategoryId = 1;
            newCampaign.StatusId = 1;
            newCampaign.CreatedBy = 1;

            _campaignService.CreateCampaign(newCampaign);
            _campaignService.SaveCampaign();

            return Json(new { isSuccess = true });
        }
    }
}
