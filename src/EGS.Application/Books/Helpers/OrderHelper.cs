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
        private static Dictionary<BookSortingFields, Expression<Func<Book, object>>> OrderKeys
            = new Dictionary<BookSortingFields, Expression<Func<Book, object>>>()
            {
                {BookSortingFields.Price,b=>b.Price },
                {BookSortingFields.Popularity,b=>b.TotalSold },
                {BookSortingFields.AddedDate,b=>b.CreateDate}
            };

        public static Expression<Func<Book, object>> GetOrder(BookSortingFields order)
        {
            return OrderKeys[order];
        }
    }
}
