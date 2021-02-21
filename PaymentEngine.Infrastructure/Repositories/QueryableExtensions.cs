﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentEngine.Infrastructure.Repositories
{

    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 1;

            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}

