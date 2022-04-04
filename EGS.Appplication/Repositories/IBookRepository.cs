﻿using EGS.Application.Common.Interfaces;
using EGS.Domain.Entities;

namespace EGS.Application.Repositories
{
    public interface IBookRepository : IRepositoryAsync<Book , long>
    {
        Task<Book> GetByISBNAsync(string isbn, CancellationToken cancellationToken);
    }
}
