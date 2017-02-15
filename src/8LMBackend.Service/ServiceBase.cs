using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _8LMBackend.DataAccess.Models;
using _8LMBackend.DataAccess.Infrastructure;

namespace _8LMBackend.Service
{
    public abstract class ServiceBase
    {
		protected DashboardDbContext DbContext
        {
            get;
			private set;
        }

        protected ServiceBase(IDbFactory dbFactory)
        {
			DbContext = dbFactory.Init();
        }
    }
}
