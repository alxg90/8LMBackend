using System;
using System.Collections.Generic;
using System.Linq;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.Service
{
    public class ProxyService : ServiceBase, IProxyService
    {
		public ProxyService(IDbFactory dbFactory)
			: base(dbFactory) 
		{
		}
        public void UpdateStatistic(PageStatistic stats, string trackingName = null)
        {
            if (trackingName != null)
            {
                var control = DbContext.ControlStat.Where(p => p.Id == stats.ControlId).FirstOrDefault();
                if (control == null)
                {
                    var item = new ControlStat()
                    {
                        Id = stats.ControlId,
                        PageId = stats.PageId,
                        Name = trackingName,
                        IsActive = true
                    };
                    DbContext.Add(item);
                } 
            }

            DbContext.PageStatistic.Add(stats);
        }  
        public void AddButtonStatistic(ControlStat stats)
        {            
            DbContext.ControlStat.Add(stats);
        }
        public void SaveDBChanges()
        {            
            DbContext.SaveChanges();
        }  
    }
}
