using System.Collections.Generic;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Models;
using System;
using _8LMBackend.Service.DTO;

namespace _8LMBackend.Service
{
    public interface IProxyService
    {
		void UpdateStatistic(PageStatistic stats, string trackingName = null);
        void AddButtonStatistic(ControlStat stats);
        void SaveDBChanges();
        List<WebClick> GetWebClicks(string userToken, DateTime fromDate, DateTime toDate);
        List<WebClickByHour> GetWebClicksByHour(string userToken, DateTime fromDate, DateTime toDate);
        List<EPageStat> GetEPageStat(string token, DateTime fromDate, DateTime toDate);
        List<EPageStatByHour> GetEPageStatByHour(string token, DateTime fromDate, DateTime toDate);
    }
}
