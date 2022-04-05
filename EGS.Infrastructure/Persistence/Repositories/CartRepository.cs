using EGS.Application.Common.Interfaces;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using EGS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EGS.Infrastructure.Persistence.Repositories
{
    public class CartRepository : CrudRepositoryAsync<ShoppingCartItem, long>, ICartRepository
    {
        private readonly DbSet<ShoppingCartHistory> _historyDbSet;
        private readonly IDateTime _dateTime;
        public CartRepository(IApplicationDbContext context, IDateTime dateTime) : base(context)
        {
            var ctx  = context as DbContext;
            if (ctx == null)
                throw new ArgumentNullException(nameof(context));

            _historyDbSet = ctx.Set<ShoppingCartHistory>();
            _dateTime = dateTime;
        }

        public override ShoppingCartItem Insert(ShoppingCartItem cartItem)
        {
            var res = base.Insert(cartItem);
            
            _historyDbSet.Add(new ShoppingCartHistory
            {
                Action = ShoppingCartHistoryAction.ItemAdded,
                BookId = cartItem.BookId,
                CustomerId = cartItem.CustomerId,
                OccuredAt = _dateTime.Now
            });

            return res;
        }

        public override ShoppingCartItem Update(ShoppingCartItem cartItem)
        {
            var res = base.Update(cartItem);

            _historyDbSet.Add(new ShoppingCartHistory
            {
                Action = ShoppingCartHistoryAction.QuantityUpdated,
                BookId = cartItem.BookId,
                CustomerId = cartItem.CustomerId,
                OccuredAt = _dateTime.Now
            });

            return res;
        }

        public override void Delete(ShoppingCartItem cartItem)
        {
            base.Delete(cartItem);

            _historyDbSet.Add(new ShoppingCartHistory
            {
                Action = ShoppingCartHistoryAction.ItemRemoved,
                BookId = cartItem.BookId,
                CustomerId = cartItem.CustomerId,
                OccuredAt = _dateTime.Now
            });
        }

    }
}
