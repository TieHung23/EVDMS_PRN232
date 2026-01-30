using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Request.Inventory;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.Inventory;

namespace EVDMS.BusinessLogicLayer.Service.Abstraction;

public interface IInventoryService
{
    Task<TResponse<InventoryListResponse>> GetListAsync(InventoryQueryRequest request, CancellationToken cancellationToken = default);
    Task<TResponse<InventoryResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TResponse<InventoryResponse>> CreateAsync(InventoryCreateRequest request, CancellationToken cancellationToken = default);
    Task<TResponse<InventoryResponse>> UpdateAsync(Guid id, InventoryUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, InventoryDeleteRequest request, CancellationToken cancellationToken = default);
}
