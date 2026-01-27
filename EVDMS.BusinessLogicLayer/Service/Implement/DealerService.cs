using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using EVDMS.Core.Model;
using EVDMS.DataAccessLayer.Repository.Abstraction;

namespace EVDMS.BusinessLogicLayer.Service.Implement;

public class DealerService : IDealerService
{
    private readonly IUnitOfWork _unitOfWork;

    public DealerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse<DealerListResponse>> GetListAsync(DealerQueryRequest request, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Dealer, Guid>();

        var hasSearch = !string.IsNullOrWhiteSpace(request.Search);
        var search = request.Search?.Trim() ?? string.Empty;

        Func<IQueryable<Dealer>, IOrderedQueryable<Dealer>>? orderBy = null;
        var sort = request.Sort?.Trim();
        if (!string.IsNullOrWhiteSpace(sort))
        {
            var isDesc = sort.StartsWith('-');
            var field = isDesc ? sort[1..] : sort;

            orderBy = field.ToLowerInvariant() switch
            {
                "code" => q => isDesc ? q.OrderByDescending(d => d.Code) : q.OrderBy(d => d.Code),
                "name" => q => isDesc ? q.OrderByDescending(d => d.Name) : q.OrderBy(d => d.Name),
                "email" => q => isDesc ? q.OrderByDescending(d => d.Email) : q.OrderBy(d => d.Email),
                "createdat" => q => isDesc ? q.OrderByDescending(d => d.CreatedAt) : q.OrderBy(d => d.CreatedAt),
                "modifiedat" => q => isDesc ? q.OrderByDescending(d => d.ModifiedAt) : q.OrderBy(d => d.ModifiedAt),
                _ => null
            };

            if (orderBy is null)
            {
                return TResponse<DealerListResponse>.Failed("Sort must be one of: code, name, email, createdAt, modifiedAt. Use '-' for desc.");
            }
        }

        var results = await repository.GetFilterAsync(
            filter: d => !hasSearch || d.Code.Contains(search) || d.Name.Contains(search) || d.Email.Contains(search),
            orderBy: orderBy,
            includeProperties: string.Empty,
            disableTracking: true,
            cancellationToken: cancellationToken);

        var all = results.ToList();
        var totalCount = all.Count;

        var list = all
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(DealerResponse.FromEntity)
            .ToList();

        var response = new DealerListResponse
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Count = list.Count,
            Items = list
        };

        return TResponse<DealerListResponse>.Success(response);
    }

    public async Task<TResponse<DealerResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Dealer, Guid>();
        var dealer = await repository.GetByIdAsync(id);
        if (dealer is null)
        {
            return TResponse<DealerResponse>.Failed("Dealer not found.");
        }

        return TResponse<DealerResponse>.Success(DealerResponse.FromEntity(dealer));
    }

    public async Task<TResponse<DealerResponse>> CreateAsync(DealerCreateRequest request, CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;
        var dealer = new Dealer(request.Code, request.Name, request.Email)
        {
            Id = Guid.NewGuid(),
            CreatedAt = now,
            CreatedAtTick = now.Ticks.ToString(),
            CreatedBy = request.CreatedBy,
            ModifiedAt = now,
            ModifiedAtTick = now.Ticks.ToString(),
            ModifiedBy = request.CreatedBy,
            IsActive = true,
            IsDeleted = false
        };

        var repository = _unitOfWork.GetRepository<Dealer, Guid>();
        await repository.AddAsync(dealer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return TResponse<DealerResponse>.Success(DealerResponse.FromEntity(dealer));
    }

    public async Task<TResponse<DealerResponse>> UpdateAsync(Guid id, DealerUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Dealer, Guid>();
        var dealer = await repository.GetByIdAsync(id);
        if (dealer is null)
        {
            return TResponse<DealerResponse>.Failed("Dealer not found.");
        }

        dealer.Code = request.Code;
        dealer.Name = request.Name;
        dealer.Email = request.Email;
        dealer.ModifiedBy = request.ModifiedBy;

        repository.Update(dealer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return TResponse<DealerResponse>.Success(DealerResponse.FromEntity(dealer));
    }

    public async Task<Response> DeleteAsync(Guid id, DealerDeleteRequest request, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Dealer, Guid>();
        var dealer = await repository.GetByIdAsync(id);
        if (dealer is null)
        {
            return Response.Failed("Dealer not found.");
        }

        dealer.ModifiedBy = request.ModifiedBy;

        repository.Delete(dealer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Success("Dealer deleted successfully.");
    }
}
