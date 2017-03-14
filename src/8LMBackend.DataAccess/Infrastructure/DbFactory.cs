using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.DataAccess.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        DashboardDbContext dbContext;

        public DashboardDbContext Init()
        {
            return dbContext ?? (dbContext = new DashboardDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
