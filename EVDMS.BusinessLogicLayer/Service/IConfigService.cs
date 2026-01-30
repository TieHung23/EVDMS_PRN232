using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Response;

namespace EVDMS.BusinessLogicLayer.Service;

public interface IConfigService
{
    Task<TResponse<ConfigListResponse>> GetAllAsync(ConfigQueryRequest request);
    Task<TResponse<ConfigResponse>> GetByIdAsync(Guid id);
    Task<TResponse<ConfigResponse>> CreateAsync(CreateConfigRequest request, string createdBy);
    Task<TResponse<ConfigResponse>> UpdateAsync(Guid id, UpdateConfigRequest request, string modifiedBy);
    Task<Response> DeleteAsync(Guid id, string modifiedBy);
}
