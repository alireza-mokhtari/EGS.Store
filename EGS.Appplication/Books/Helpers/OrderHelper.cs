using EGS.Domain.Entities;
using EGS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EGS.Application.Books.Helpers
{
    public static class OrderHelper
    {
        private static Dictionary<SearchOrder, Expression<Func<Book, object>>> OrderKeys
            = new Dictionary<SearchOrder, Expression<Func<Book, object>>>()
            {
                {SearchOrder.Price,b=>b.Price },
                {SearchOrder.Popularity,b=>b.TotalSold },
                {SearchOrder.AddedDate,b=>b.CreateDate}
            };

        public static Expression<Func<Book, object>> GetOrder(SearchOrder order)
        {
            return OrderKeys[order];
        }
    }
}
