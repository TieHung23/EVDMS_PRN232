using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Request.Vehicle;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.Vehicle;
using EVDMS.BusinessLogicLayer.Helper;
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

        var results = await repository.GetFilterAsync(
            filter: v => (string.IsNullOrEmpty(request.ModelName) || v.ModelName.Contains(request.ModelName)) &&
                         (string.IsNullOrEmpty(request.Brand) || v.Brand.Contains(request.Brand)) &&
                         (string.IsNullOrEmpty(request.VehicleType) || v.VehicleType.Contains(request.VehicleType)) &&
                         (!request.ReleaseYear.HasValue || v.ReleaseYear == request.ReleaseYear.Value),
            includeProperties: string.Empty,
            disableTracking: true,
            cancellationToken: cancellationToken);

        var all = results.ToList();
        var totalCount = all.Count;
        var (skip, take) = RequestExtensions.ToSkipTake(request.PageNumber, request.PageSize);

        var list = all
            .Skip(skip)
            .Take(take)
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
        vehicle.ModifiedBy = vehicle.ModifiedBy;
        repository.Delete(vehicle);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Success("Vehicle deleted successfully.");
    }
}
