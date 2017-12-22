using System;
using System.Collections.Generic;
using System.Linq;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using _8LMBackend.DataAccess.Models;
using _8LMBackend.Service.DTO;

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

        public int GetUserID(string access_token)
        {
            var u = DbContext.UserToken.Where(p => p.Token == access_token).FirstOrDefault();
            if (u == default(UserToken))
                throw new Exception("Not authorized");

            return u.UserId;
        }

        public List<WebClick> GetWebClicks(string userToken, DateTime fromDate, DateTime toDate)
        {
            int UserID = GetUserID(userToken);

            List<WebClick> result = new List<WebClick>();
            var stat = DbContext.PageStatistic.Join(DbContext.Pages.Where(pg => pg.CreatedBy == UserID), ps => ps.PageId, p => p.Id, (ps, p) => ps).Where(ps => ps.IsLoad == null && ps.CreatedDate.Date >= fromDate.Date && ps.CreatedDate.Date <= toDate.Date).GroupBy(ps => ps.CreatedDate.Date).Select(g => new { date = g.Key, count = g.Count() } );
            foreach (var click in stat)
            {
                WebClick item = new WebClick()
                {
                    date = click.date.Year.ToString() + "-" + click.date.Month.ToString() + "-" + click.date.Day.ToString(),
                    clicks = click.count
                };
                result.Add(item);
            }

            return result;
        }

        public List<WebClickByHour> GetWebClicksByHour(string userToken, DateTime fromDate, DateTime toDate)
        {
            int UserID = GetUserID(userToken);

            List<WebClickByHour> result = new List<WebClickByHour>();
            var stat = DbContext.PageStatistic.Join(DbContext.Pages.Where(pg => pg.CreatedBy == UserID), ps => ps.PageId, p => p.Id, (ps, p) => ps).Where(ps => ps.IsLoad == null && ps.CreatedDate.Date >= fromDate.Date && ps.CreatedDate.Date <= toDate.Date).GroupBy(x => new { x.CreatedDate.Date, x.CreatedDate.Hour }).Select(g => new { groupingKey = g.Key, count = g.Count() }).OrderBy(d => d.groupingKey.Date);
            string dt = string.Empty;
            WebClickByHour item = null;
            foreach (var click in stat)
            {
                string d = click.groupingKey.Date.Year.ToString() + "-" + click.groupingKey.Date.Month.ToString() + "-" + click.groupingKey.Date.Day.ToString();
                if (d != dt)
                {
                    if (item != null)
                        result.Add(item);

                    dt = d;
                    item = new WebClickByHour();
                    item.date = d;
                    item.clicks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                }
                item.clicks[click.groupingKey.Hour] = click.count;
            }
            if (item != null)
                result.Add(item);

            return result;
        }
    }
}
