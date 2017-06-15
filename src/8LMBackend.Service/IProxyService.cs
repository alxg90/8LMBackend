using System.Collections.Generic;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Models;
using System;

namespace _8LMBackend.Service
{
    public interface IProxyService
    {
		void UpdateStatistic(PageStatistic stats, string trackingName = null);
        void AddButtonStatistic(ControlStat stats);
        void SaveDBChanges();
    }
}
