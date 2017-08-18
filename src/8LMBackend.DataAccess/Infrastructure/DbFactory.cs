using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.DataAccess.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        DashboardDbContext dbContext;
        DevelopmentDbContext DevDbContext;

        public DashboardDbContext Init()
        {
            return dbContext ?? (dbContext = new DashboardDbContext());
        }

        public DevelopmentDbContext InitDev()
        {
            return DevDbContext ?? (DevDbContext = new DevelopmentDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();

            if (DevDbContext != null)
                DevDbContext.Dispose();
        }
    }
}
