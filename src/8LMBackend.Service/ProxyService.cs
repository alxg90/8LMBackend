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

        public List<EPageStat> GetEPageStat(string token, DateTime fromDate, DateTime toDate)
        {
            int UserID = GetUserID(token);

            Dictionary<string, int> dViews = new Dictionary<string, int>();
            var views = DevDbContext.dist_epage_views.Where(p => p.dist_id == UserID && p.created_at.Date >= fromDate.Date && p.created_at <= toDate.Date).GroupBy(p => p.created_at.Date).Select(g => new { date = g.Key, count = g.Count() });
            foreach (var v in views)
            {
                dViews.Add(v.date.Year.ToString() + "-" + v.date.Month.ToString() + "-" + v.date.Day.ToString(), v.count);
            }

            Dictionary<string, int> dClicks = new Dictionary<string, int>();
            var clicks = DevDbContext.dist_epage_clicks.Where(p => p.dist_id == UserID && p.created_at.Date >= fromDate.Date && p.created_at <= toDate.Date).GroupBy(p => p.created_at.Date).Select(g => new { date = g.Key, count = g.Count() });
            foreach (var c in clicks)
            {
                dClicks.Add(c.date.Year.ToString() + "-" + c.date.Month.ToString() + "-" + c.date.Day.ToString(), c.count);
            }

            List<EPageStat> result = new List<EPageStat>();
            foreach (var v in dViews)
            {
                EPageStat item = new EPageStat();
                item.date = v.Key;
                item.views = v.Value;
                item.clicks = dClicks.ContainsKey(v.Key) ? dClicks[v.Key] : 0;
                result.Add(item);
            }

            foreach (var c in dClicks)
            {
                if (!result.Exists(p => p.date == c.Key))
                {
                    EPageStat item = new EPageStat();
                    item.date = c.Key;
                    item.views = 0;
                    item.clicks = c.Value;
                    result.Add(item);
                }
            }

            return result;
        }

        public List<EPageStatByHour> GetEPageStatByHour(string token, DateTime fromDate, DateTime toDate)
        {
            int UserID = GetUserID(token);

            /*views*/
            Dictionary<string, int[]> dViews = new Dictionary<string, int[]>();
            string dt = string.Empty;
            int[] item = null;
            var views = DevDbContext.dist_epage_views.Where(p => p.dist_id == UserID && p.created_at.Date >= fromDate.Date && p.created_at <= toDate.Date).GroupBy(x => new { x.created_at.Date, x.created_at.Hour }).Select(g => new { groupingKey = g.Key, count = g.Count() }).OrderBy(d => d.groupingKey.Date);
            foreach (var v in views)
            {
                string d = v.groupingKey.Date.Year.ToString() + "-" + v.groupingKey.Date.Month.ToString() + "-" + v.groupingKey.Date.Day.ToString();
                if (d != dt)
                {
                    dt = d;
                    item = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                }
                item[v.groupingKey.Hour] = v.count;

                if (dViews.ContainsKey(d))
                    dViews[d] = item;
                else
                    dViews.Add(d, item);
            }

            /*clicks*/
            Dictionary<string, int[]> dClicks = new Dictionary<string, int[]>();
            dt = string.Empty;
            item = null;
            var clicks = DevDbContext.dist_epage_clicks.Where(p => p.dist_id == UserID && p.created_at.Date >= fromDate.Date && p.created_at <= toDate.Date).GroupBy(x => new { x.created_at.Date, x.created_at.Hour }).Select(g => new { groupingKey = g.Key, count = g.Count() }).OrderBy(d => d.groupingKey.Date);
            foreach (var c in clicks)
            {
                string d = c.groupingKey.Date.Year.ToString() + "-" + c.groupingKey.Date.Month.ToString() + "-" + c.groupingKey.Date.Day.ToString();
                if (d != dt)
                {
                    dt = d;
                    item = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                }
                item[c.groupingKey.Hour] = c.count;

                if (dClicks.ContainsKey(d))
                    dClicks[d] = item;
                else
                    dClicks.Add(d, item);
            }

            /*merging*/
            List<EPageStatByHour> result = new List<EPageStatByHour>();
            foreach (var dv in dViews)
            {
                EPageStatByHour r = new EPageStatByHour();
                r.date = dv.Key;
                r.views = dv.Value;
                r.clicks = dClicks.ContainsKey(dv.Key) ? dClicks[dv.Key] : new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                result.Add(r);
            }

            foreach (var dc in dClicks)
            {
                if (!result.Exists(p => p.date == dc.Key))
                {
                    EPageStatByHour r = new EPageStatByHour();
                    r.date = dc.Key;
                    r.views = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    r.clicks = dc.Value;
                    result.Add(r);
                }
            }

            return result;
        }
    }
}
