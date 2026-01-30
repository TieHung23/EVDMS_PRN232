using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Helper;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using EVDMS.Core.Model;
using EVDMS.DataAccessLayer.Repository.Abstraction;

namespace EVDMS.BusinessLogicLayer.Service.Implement;

public class InventoryService : IInventoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public InventoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse<InventoryListResponse>> GetListAsync(InventoryQueryRequest request, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Inventory, Guid>();

        var results = await repository.GetFilterAsync(
            filter: i => (!request.DealerId.HasValue || i.DealerId == request.DealerId.Value) &&
                         (!request.VehicleId.HasValue || i.VehicleId == request.VehicleId.Value) &&
                         (!request.MinQuantity.HasValue || i.Quantity >= request.MinQuantity.Value),
            includeProperties: "Dealer,Vehicle",
            disableTracking: true,
            cancellationToken: cancellationToken);

        var all = results.ToList();
        var totalCount = all.Count;
        var (skip, take) = RequestExtensions.ToSkipTake(request.PageNumber, request.PageSize);

        var list = all
            .Skip(skip)
            .Take(take)
            .Select(InventoryResponse.FromEntity)
            .ToList();

        var response = new InventoryListResponse
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Count = list.Count,
            Items = list
        };

        return TResponse<InventoryListResponse>.Success(response);
    }

    public async Task<TResponse<InventoryResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Inventory, Guid>();
        var inventory = await repository.GetFirstOrDefaultAsync(
            filter: i => i.Id == id,
            includeProperties: "Dealer,Vehicle",
            disableTracking: true,
            cancellationToken: cancellationToken);

        if (inventory is null)
        {
            return TResponse<InventoryResponse>.Failed("Inventory not found.");
        }

        return TResponse<InventoryResponse>.Success(InventoryResponse.FromEntity(inventory));
    }
}
