using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Domain.Entities;
using Mapster;

namespace EGS.Application.Repositories
{
    public interface ICartRepository : ICrudRepositoryAsync<ShoppingCartItem, long>
    {
        Task<List<ShoppingCartItemDto>> GetCartItems(string customerId, CancellationToken cancellationToken);
    }
}
