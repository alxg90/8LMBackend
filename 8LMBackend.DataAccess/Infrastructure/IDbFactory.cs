using _8LMBackend.DataAccess.Models;
using System;

namespace _8LMBackend.DataAccess.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        DashboardDbContext Init();
    }
}
