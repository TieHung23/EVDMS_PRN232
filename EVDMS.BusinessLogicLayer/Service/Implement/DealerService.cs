using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.Dealer;
using EVDMS.BusinessLogicLayer.Helper;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using EVDMS.Core.Model;
using EVDMS.DataAccessLayer.Const;
using EVDMS.DataAccessLayer.Repository.Abstraction;

namespace EVDMS.BusinessLogicLayer.Service.Implement;

public class DealerService : IDealerService
{
    private readonly IUnitOfWork _unitOfWork;

    public DealerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<TResponse<List<DealerResponse>>> GetAllDealersAsync()
    {
        var dealers = await _unitOfWork.GetRepository<Dealer, Guid>().GetAllAsync();

        var dealerResponses = Mapper.CreateDealerResponseList(dealers.ToList());

        return TResponse<List<DealerResponse>>.Success(dealerResponses, Const.GetSuccessMessage(Const.NameOfClasses.Dealer));
    }

    public async Task<TResponse<DealerResponse>> GetDealerByIdAsync(Guid id)
    {
        var dealer = await _unitOfWork.GetRepository<Dealer, Guid>().GetByIdAsync(id);

        if (dealer == null)
        {
            return TResponse<DealerResponse>.Failed("Dealer not found");
        }

        var dealerResponse = Mapper.CreateDealerResponse(dealer);

        return TResponse<DealerResponse>.Success(dealerResponse, Const.GetSuccessMessage(Const.NameOfClasses.Dealer));
    }
}