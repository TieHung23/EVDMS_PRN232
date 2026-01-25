using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.Dealer;

namespace EVDMS.BusinessLogicLayer.Service.Abstraction;

public interface IDealerService
{
    Task<TResponse<List<DealerResponse>>> GetAllDealersAsync();

    Task<TResponse<DealerResponse>> GetDealerByIdAsync(Guid id);
}