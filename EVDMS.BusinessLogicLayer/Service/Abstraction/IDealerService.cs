using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Response;

namespace EVDMS.BusinessLogicLayer.Service.Abstraction;

public interface IDealerService
{
    Task<TResponse<DealerListResponse>> GetListAsync(DealerQueryRequest request, CancellationToken cancellationToken = default);
    Task<TResponse<DealerResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TResponse<DealerResponse>> CreateAsync(DealerCreateRequest request, CancellationToken cancellationToken = default);
    Task<TResponse<DealerResponse>> UpdateAsync(Guid id, DealerUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, DealerDeleteRequest request, CancellationToken cancellationToken = default);
}
