using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Repositories;
using EGS.Domain.Entities;
using EGS.Domain.Enums;
using MapsterMapper;

namespace EGS.Application.Inventory.Commands.AddStockCommand
{
    public class AddStockCommand : IRequestWrapper<InventoryTransactionDto>
    {
        public long? BookId { get; set; }
        public int Quantity { get; set; }
    }

    public partial class AddStockCommandHandler : IRequestHandlerWrapper<AddStockCommand, InventoryTransactionDto>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public AddStockCommandHandler(IInventoryRepository inventoryRepository, IDateTime dateTime, 
            ICurrentUserService currentUserService, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<InventoryTransactionDto>> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            // TODO: Watch out pesimistic concurrency (maybe singleton thread safe repository)

            var inventoryTransaction = new InventoryTransaction
            {
                BookId = request.BookId.Value,
                Incremented = request.Quantity,
                ModifiedOn = _dateTime.Now,
                Reason = InventoryTransactionType.ManualEntry,
                Stock = await _inventoryRepository.GetStock(request.BookId.Value, cancellationToken) + request.Quantity,
                UserId = _currentUserService.UserId
            };

            _inventoryRepository.Insert(inventoryTransaction);
            await _inventoryRepository.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<InventoryTransactionDto>(inventoryTransaction));
        }
    }
}
