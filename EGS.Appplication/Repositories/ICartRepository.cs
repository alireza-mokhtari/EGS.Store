using EGS.Application.Common.Interfaces;
using EGS.Application.Dto;
using EGS.Domain.Entities;
using Mapster;

namespace EGS.Application.Repositories
{
    public interface ICartRepository : ICrudRepositoryAsync<ShoppingCartItem, long>
    {
        
    }
}
