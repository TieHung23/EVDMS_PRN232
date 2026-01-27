using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using EVDMS.Core.Model;
using EVDMS.DataAccessLayer.Repository.Abstraction;

namespace EVDMS.BusinessLogicLayer.Service.Implement;

public class VehicleService : IVehicleService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehicleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse<VehicleListResponse>> GetListAsync(VehicleQueryRequest request, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Vehicle, Guid>();

        var hasSearch = !string.IsNullOrWhiteSpace(request.Search);
        var search = request.Search?.Trim() ?? string.Empty;

        Func<IQueryable<Vehicle>, IOrderedQueryable<Vehicle>>? orderBy = null;
        var sort = request.Sort?.Trim();
        if (!string.IsNullOrWhiteSpace(sort))
        {
            var isDesc = sort.StartsWith('-');
            var field = isDesc ? sort[1..] : sort;

            orderBy = field.ToLowerInvariant() switch
            {
                "modelname" => q => isDesc ? q.OrderByDescending(v => v.ModelName) : q.OrderBy(v => v.ModelName),
                "brand" => q => isDesc ? q.OrderByDescending(v => v.Brand) : q.OrderBy(v => v.Brand),
                "vehicletype" => q => isDesc ? q.OrderByDescending(v => v.VehicleType) : q.OrderBy(v => v.VehicleType),
                "releaseyear" => q => isDesc ? q.OrderByDescending(v => v.ReleaseYear) : q.OrderBy(v => v.ReleaseYear),
                "createdat" => q => isDesc ? q.OrderByDescending(v => v.CreatedAt) : q.OrderBy(v => v.CreatedAt),
                "modifiedat" => q => isDesc ? q.OrderByDescending(v => v.ModifiedAt) : q.OrderBy(v => v.ModifiedAt),
                _ => null
            };

            if (orderBy is null)
            {
                return TResponse<VehicleListResponse>.Failed("Sort must be one of: modelName, brand, vehicleType, releaseYear, createdAt, modifiedAt. Use '-' for desc.");
            }
        }

        var results = await repository.GetFilterAsync(
            filter: v => !hasSearch
                         || v.ModelName.Contains(search)
                         || v.Brand.Contains(search)
                         || v.VehicleType.Contains(search),
            orderBy: orderBy,
            includeProperties: string.Empty,
            disableTracking: true,
            cancellationToken: cancellationToken);

        var all = results.ToList();
        var totalCount = all.Count;

        var list = all
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(VehicleResponse.FromEntity)
            .ToList();

        var response = new VehicleListResponse
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Count = list.Count,
            Items = list
        };

        return TResponse<VehicleListResponse>.Success(response);
    }

    public async Task<TResponse<VehicleResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Vehicle, Guid>();
        var vehicle = await repository.GetByIdAsync(id);
        if (vehicle is null)
        {
            return TResponse<VehicleResponse>.Failed("Vehicle not found.");
        }

        return TResponse<VehicleResponse>.Success(VehicleResponse.FromEntity(vehicle));
    }

    public async Task<TResponse<VehicleResponse>> CreateAsync(VehicleCreateRequest request, CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;
        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            ModelName = request.ModelName,
            Brand = request.Brand,
            VehicleType = request.VehicleType,
            Description = request.Description,
            ReleaseYear = request.ReleaseYear,
            CreatedAt = now,
            CreatedAtTick = now.Ticks.ToString(),
            CreatedBy = request.CreatedBy,
            ModifiedAt = now,
            ModifiedAtTick = now.Ticks.ToString(),
            ModifiedBy = request.CreatedBy,
            IsActive = true,
            IsDeleted = false
        };

        var repository = _unitOfWork.GetRepository<Vehicle, Guid>();
        await repository.AddAsync(vehicle);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return TResponse<VehicleResponse>.Success(VehicleResponse.FromEntity(vehicle));
    }

    public async Task<TResponse<VehicleResponse>> UpdateAsync(Guid id, VehicleUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Vehicle, Guid>();
        var vehicle = await repository.GetByIdAsync(id);
        if (vehicle is null)
        {
            return TResponse<VehicleResponse>.Failed("Vehicle not found.");
        }

        vehicle.ModelName = request.ModelName;
        vehicle.Brand = request.Brand;
        vehicle.VehicleType = request.VehicleType;
        vehicle.Description = request.Description;
        vehicle.ReleaseYear = request.ReleaseYear;
        vehicle.ModifiedBy = request.ModifiedBy;

        repository.Update(vehicle);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return TResponse<VehicleResponse>.Success(VehicleResponse.FromEntity(vehicle));
    }

    public async Task<Response> DeleteAsync(Guid id, VehicleDeleteRequest request, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Vehicle, Guid>();
        var vehicle = await repository.GetByIdAsync(id);
        if (vehicle is null)
        {
            return Response.Failed("Vehicle not found.");
        }

        vehicle.ModifiedBy = request.ModifiedBy;
        repository.Delete(vehicle);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Success("Vehicle deleted successfully.");
    }
}
