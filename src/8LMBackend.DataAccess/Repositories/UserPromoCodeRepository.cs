﻿using _8LMBackend.DataAccess.Infrastructure;
using _8LMBackend.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8LMBackend.DataAccess.Repositories
{
    public class UserPromoCodeRepository : RepositoryBase<Userpromocode>, IUserPromoCodeRepository
    {
        public UserPromoCodeRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }
}