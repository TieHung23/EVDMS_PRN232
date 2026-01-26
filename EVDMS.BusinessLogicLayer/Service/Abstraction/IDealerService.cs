using EVDMS.BusinessLogicLayer.Dto.Request.Dealer;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.Dealer;

namespace EVDMS.BusinessLogicLayer.Service.Abstraction;

public interface IDealerService
{
    Task<TResponse<DealerResponse>> GetAllDealersAsync(DealerGetFilter filter);

    Task<TResponse<DealerResponse>> GetDealerByIdAsync(Guid id);
}