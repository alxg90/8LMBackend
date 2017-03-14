using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.DataAccess.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private DashboardDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public DashboardDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
