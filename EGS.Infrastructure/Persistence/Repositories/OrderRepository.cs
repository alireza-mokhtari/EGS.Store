using EGS.Application.Common.Interfaces;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace EGS.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : CrudRepositoryAsync<Order, long>, IOrderRepository
    {        
        private readonly IDateTime _dateTime;
        private readonly IMapper _mapper;
        public OrderRepository(IApplicationDbContext context, IDateTime dateTime, IMapper mapper) : base(context)
        {
            _dateTime = dateTime;
            _mapper = mapper;
        }
    }
}
