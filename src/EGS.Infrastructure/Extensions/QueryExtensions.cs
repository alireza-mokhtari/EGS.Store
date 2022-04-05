using EGS.Application.Common.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EGS.Infrastructure.Extensions
{
    public static class QueryExtensions
    {
        public static async Task<PaginatedList<T>> CreateAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var count = await source.CountAsync(cancellationToken);
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, CancellationToken cancellationToken)
             => CreateAsync(queryable, pageNumber, pageSize, cancellationToken);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, TypeAdapterConfig configuration, CancellationToken cancellationToken)
            => queryable.ProjectToType<TDestination>(configuration).ToListAsync(cancellationToken);

    }
}
