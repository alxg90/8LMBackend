using System;
using System.Threading;
using System.Linq;
using _8LMBackend.Service.ViewModels;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using _8LMBackend.DataAccess.Models;

namespace _8LMBackend.Service
{
    public class CheckPaymentService : ServiceBase
    {
        public CheckPaymentService(IDbFactory dbFactory)
			: base(dbFactory) 
		{
		}
        
    }
}
