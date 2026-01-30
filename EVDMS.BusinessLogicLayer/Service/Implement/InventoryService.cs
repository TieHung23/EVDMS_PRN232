using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Request.Inventory;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.Inventory;
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

    public async Task<TResponse<InventoryResponse>> CreateAsync(InventoryCreateRequest request, CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;
        var inventory = new Inventory
        {
            Id = Guid.NewGuid(),
            DealerId = request.DealerId,
            VehicleId = request.VehicleId,
            Quantity = request.Quantity,
            CreatedAt = now,
            CreatedAtTick = now.Ticks.ToString(),
            CreatedBy = request.CreatedBy,
            ModifiedAt = now,
            ModifiedAtTick = now.Ticks.ToString(),
            ModifiedBy = request.CreatedBy,
            IsActive = true,
            IsDeleted = false
        };

        var repository = _unitOfWork.GetRepository<Inventory, Guid>();
        await repository.AddAsync(inventory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Reload with related entities
        var created = await repository.GetFirstOrDefaultAsync(
            filter: i => i.Id == inventory.Id,
            includeProperties: "Dealer,Vehicle",
            disableTracking: true,
            cancellationToken: cancellationToken);

        return TResponse<InventoryResponse>.Success(InventoryResponse.FromEntity(created!));
    }

    public async Task<TResponse<InventoryResponse>> UpdateAsync(Guid id, InventoryUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Inventory, Guid>();
        var inventory = await repository.GetByIdAsync(id);
        if (inventory is null)
        {
            return TResponse<InventoryResponse>.Failed("Inventory not found.");
        }

        var now = DateTime.Now;
        inventory.DealerId = request.DealerId;
        inventory.VehicleId = request.VehicleId;
        inventory.Quantity = request.Quantity;
        inventory.ModifiedAt = now;
        inventory.ModifiedAtTick = now.Ticks.ToString();
        inventory.ModifiedBy = request.ModifiedBy;

        repository.Update(inventory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Reload with related entities
        var updated = await repository.GetFirstOrDefaultAsync(
            filter: i => i.Id == id,
            includeProperties: "Dealer,Vehicle",
            disableTracking: true,
            cancellationToken: cancellationToken);

        return TResponse<InventoryResponse>.Success(InventoryResponse.FromEntity(updated!));
    }

    public async Task<Response> DeleteAsync(Guid id, InventoryDeleteRequest request, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Inventory, Guid>();
        var inventory = await repository.GetByIdAsync(id);
        if (inventory is null)
        {
            return Response.Failed("Inventory not found.");
        }
        inventory.ModifiedBy = request.ModifiedBy;
        repository.Delete(inventory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Success("Inventory deleted successfully.");
    }
}
