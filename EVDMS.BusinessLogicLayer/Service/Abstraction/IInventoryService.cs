using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Response;

namespace EVDMS.BusinessLogicLayer.Service.Abstraction;

public interface IInventoryService
{
    Task<TResponse<InventoryListResponse>> GetListAsync(InventoryQueryRequest request, CancellationToken cancellationToken = default);
    Task<TResponse<InventoryResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
