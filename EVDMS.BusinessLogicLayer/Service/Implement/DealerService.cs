using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Request.Dealer;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.Dealer;
using EVDMS.BusinessLogicLayer.Helper;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using EVDMS.Core.Model;
using EVDMS.DataAccessLayer.Const;
using EVDMS.DataAccessLayer.Repository.Abstraction;
using Microsoft.Extensions.Logging;

namespace EVDMS.BusinessLogicLayer.Service.Implement;

public class DealerService : IDealerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DealerService> _logger;

    public DealerService(IUnitOfWork unitOfWork, ILogger<DealerService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<TResponse<DealerResponse>> GetAllDealersAsync(DealerGetFilter filter)
    {
        var (skip, take) = RequestExtensions.ToSkipTake(filter.PageNumber, filter.PageSize);

        var dealers = await _unitOfWork
            .GetRepository<Dealer, Guid>()
            .GetFilterAsync(filter:dealer =>
                (string.IsNullOrEmpty(filter.Name)  || dealer.Name.Contains(filter.Name)) &&
                (string.IsNullOrEmpty(filter.Code)  || dealer.Code.Contains(filter.Code)) &&
                (string.IsNullOrEmpty(filter.Email) || dealer.Email.Contains(filter.Email)),
                skip: skip,
                take: take
            );

        var dealerDtos = Mapper.CreateDealerResponseList(dealers.ToList());

        var dealerResponse = new DealerResponse
        {
            Dealers =  dealerDtos,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize,
        };

        return TResponse<DealerResponse>.Success(dealerResponse,
            Const.GetSuccessMessage(Const.NameOfClasses.Dealer));
    }

    public async Task<TResponse<DealerResponse>> GetDealerByIdAsync(Guid id)
    {
        var dealer = await _unitOfWork.GetRepository<Dealer, Guid>().GetByIdAsync(id);

        if (dealer == null)
        {
            return TResponse<DealerResponse>.Failed("Dealer not found");
        }

        var dealerDto = Mapper.CreateDealerResponse(dealer);

        var dealerResponse = new DealerResponse
        {
            Dealers = [dealerDto],
        };

        return TResponse<DealerResponse>.Success(dealerResponse, Const.GetSuccessMessage(Const.NameOfClasses.Dealer));
    }
}