using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Request.Vehicle;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.Vehicle;

namespace EVDMS.BusinessLogicLayer.Service.Abstraction;

public interface IVehicleService
{
    Task<TResponse<VehicleListResponse>> GetListAsync(VehicleQueryRequest request, CancellationToken cancellationToken = default);
    Task<TResponse<VehicleResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TResponse<VehicleResponse>> CreateAsync(VehicleCreateRequest request, CancellationToken cancellationToken = default);
    Task<TResponse<VehicleResponse>> UpdateAsync(Guid id, VehicleUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, VehicleDeleteRequest request, CancellationToken cancellationToken = default);
}
