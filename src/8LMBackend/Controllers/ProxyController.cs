using _8LMBackend.DataAccess.Models;
using _8LMBackend.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using _8LMBackend.Service.DTO;

namespace _8LMCore.Controllers
{
    public class TempControlStats
    {
        public string Id { get; set; }
        public string Name { get;set;}
        public bool IsActive {get;set;}
        public bool IsTracked {get;set;}
    }
    public class ProxyController : Controller
    {
        private readonly IProxyService _proxyService;

        public ProxyController(IProxyService proxyService)
        {
            _proxyService = proxyService;
        }
        public ActionResult UpdateStatistic(string url, string controlId, int pageId, string trackingName)
        {            
            try
            {
                var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
                PageStatistic stats = new PageStatistic();
                stats.ControlId = controlId.Substring(0, 32);
                stats.CreatedDate = DateTime.UtcNow;
                stats.PageId = pageId;
                //stats.Ip = remoteIpAddress.ToString();
                _proxyService.UpdateStatistic(stats, trackingName);
                _proxyService.SaveDBChanges();
                return Redirect(url);
            }
            catch (System.Exception ex)
            {
                return Redirect(url);
            }
        }

        public void IsPageLoad(int pageId, bool? isLoad)
        {            
            try
            {
                var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
                PageStatistic stats = new PageStatistic();
                stats.CreatedDate = new DateTime();
                stats.PageId = pageId;
                stats.IsLoad = isLoad;
                stats.CreatedDate = DateTime.UtcNow;
                //stats.Ip = remoteIpAddress.ToString();
                _proxyService.UpdateStatistic(stats);
                _proxyService.SaveDBChanges();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveButtonsToStats(string buttonsArray, int pageId)
        {
            try
            {
                TempControlStats[] items = JsonConvert.DeserializeObject<TempControlStats[]>(buttonsArray);
                foreach(var item in items)
                {
                    if(item.IsTracked)
                    {
                        ControlStat stats = new ControlStat();                    
                        stats.Name = item.Name;
                        stats.PageId = pageId;
                        stats.Id = item.Id;
                        stats.IsActive = item.IsActive;                    
                        _proxyService.AddButtonStatistic(stats);
                    }
                }
                _proxyService.SaveDBChanges();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void SaveImageFiles(List<Microsoft.AspNetCore.Http.IFormFile> files)
        {
            FileManager manager = new FileManager();
            manager.writeFiles(files);
        }
        public ViewResult SaveImage()
        {
            return View();
        }

        public WebClick[] GetWebClicks(string token, DateTime fromDate, DateTime toDate)
        {
            return _proxyService.GetWebClicks(token, fromDate, toDate).ToArray();
        }

        public WebClickByHour[] GetWebClicksByHour(string token, DateTime fromDate, DateTime toDate)
        {
            return _proxyService.GetWebClicksByHour(token, fromDate, toDate).ToArray();
        }

        public EPageStat[] GetEPageStat(string token, DateTime fromDate, DateTime toDate)
        {
            return _proxyService.GetEPageStat(token, fromDate, toDate).ToArray();
        }

        public EPageStatByHour[] GetEPageStatByHour(string token, DateTime fromDate, DateTime toDate)
        {
            return _proxyService.GetEPageStatByHour(token, fromDate, toDate).ToArray();
        }
    }
}
