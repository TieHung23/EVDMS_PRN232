using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.Core.Model;
using EVDMS.DataAccessLayer.Repository.Abstraction;

namespace EVDMS.BusinessLogicLayer.Service;

public class ConfigService : IConfigService
{
    private readonly IUnitOfWork _unitOfWork;

    public ConfigService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse<ConfigListResponse>> GetAllAsync(ConfigQueryRequest request)
    {
        var repository = _unitOfWork.GetRepository<Config, Guid>();

        var configs = await repository.GetAllAsync(
            filter: c => !c.IsDeleted
                && (string.IsNullOrEmpty(request.SearchTerm)
                    || c.Name.Contains(request.SearchTerm)
                    || c.Description.Contains(request.SearchTerm))
                && (!request.VehicleId.HasValue || c.VehicleId == request.VehicleId)
                && (!request.IsActive.HasValue || c.IsActive == request.IsActive),
            includeProperties: "Vehicle"
        );

        var configList = configs.ToList();

        // Sorting
        if (!string.IsNullOrEmpty(request.SortBy))
        {
            configList = request.SortBy.ToLower() switch
            {
                "name" => request.SortDescending
                    ? configList.OrderByDescending(c => c.Name).ToList()
                    : configList.OrderBy(c => c.Name).ToList(),
                "createdat" => request.SortDescending
                    ? configList.OrderByDescending(c => c.CreatedAt).ToList()
                    : configList.OrderBy(c => c.CreatedAt).ToList(),
                "modifiedat" => request.SortDescending
                    ? configList.OrderByDescending(c => c.ModifiedAt).ToList()
                    : configList.OrderBy(c => c.ModifiedAt).ToList(),
                _ => configList.OrderByDescending(c => c.CreatedAt).ToList()
            };
        }
        else
        {
            configList = configList.OrderByDescending(c => c.CreatedAt).ToList();
        }

        var totalItems = configList.Count;
        var totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);

        // Parse selected fields
        var selectedFields = string.IsNullOrWhiteSpace(request.Fields)
            ? null
            : request.Fields.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var pagedConfigs = configList
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(c => MapToResponse(c).ToSelectedFields(selectedFields))
            .ToList();

        var response = new ConfigListResponse
        {
            Items = pagedConfigs,
            Page = request.PageNumber,
            PageSize = request.PageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };

        return TResponse<ConfigListResponse>.Success(response);
    }

    public async Task<TResponse<ConfigResponse>> GetByIdAsync(Guid id)
    {
        var repository = _unitOfWork.GetRepository<Config, Guid>();

        var config = await repository.GetFirstOrDefaultAsync(
            filter: c => c.Id == id && !c.IsDeleted,
            includeProperties: "Vehicle"
        );

        if (config == null)
        {
            return TResponse<ConfigResponse>.Failed($"Config with ID '{id}' not found.");
        }

        return TResponse<ConfigResponse>.Success(MapToResponse(config));
    }

    public async Task<TResponse<ConfigResponse>> CreateAsync(CreateConfigRequest request, string createdBy)
    {
        var repository = _unitOfWork.GetRepository<Config, Guid>();
        var vehicleRepository = _unitOfWork.GetRepository<Vehicle, Guid>();

        // Validate Vehicle exists
        var vehicle = await vehicleRepository.GetByIdAsync(request.VehicleId);
        if (vehicle == null)
        {
            return TResponse<ConfigResponse>.Failed($"Vehicle with ID '{request.VehicleId}' not found.");
        }

        var now = DateTime.Now;
        var config = new Config
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            VehicleId = request.VehicleId,
            CreatedAt = now,
            CreatedAtTick = now.Ticks.ToString(),
            CreatedBy = createdBy,
            ModifiedAt = now,
            ModifiedAtTick = now.Ticks.ToString(),
            ModifiedBy = createdBy,
            IsActive = true,
            IsDeleted = false
        };

        await repository.AddAsync(config);
        await _unitOfWork.SaveChangesAsync();

        config.Vehicle = vehicle;
        return TResponse<ConfigResponse>.Success(MapToResponse(config), "Config created successfully.");
    }

    public async Task<TResponse<ConfigResponse>> UpdateAsync(Guid id, UpdateConfigRequest request, string modifiedBy)
    {
        var repository = _unitOfWork.GetRepository<Config, Guid>();
        var vehicleRepository = _unitOfWork.GetRepository<Vehicle, Guid>();

        var config = await repository.GetFirstOrDefaultAsync(
            filter: c => c.Id == id && !c.IsDeleted,
            includeProperties: "Vehicle"
        );

        if (config == null)
        {
            return TResponse<ConfigResponse>.Failed($"Config with ID '{id}' not found.");
        }

        // Validate Vehicle exists if VehicleId is different
        if (request.VehicleId != config.VehicleId)
        {
            var vehicle = await vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle == null)
            {
                return TResponse<ConfigResponse>.Failed($"Vehicle with ID '{request.VehicleId}' not found.");
            }
            config.Vehicle = vehicle;
        }

        config.Name = request.Name;
        config.Description = request.Description;
        config.VehicleId = request.VehicleId;
        config.ModifiedBy = modifiedBy;

        repository.Update(config);
        await _unitOfWork.SaveChangesAsync();

        return TResponse<ConfigResponse>.Success(MapToResponse(config), "Config updated successfully.");
    }

    public async Task<Response> DeleteAsync(Guid id, string modifiedBy)
    {
        var repository = _unitOfWork.GetRepository<Config, Guid>();

        var config = await repository.GetFirstOrDefaultAsync(
            filter: c => c.Id == id && !c.IsDeleted
        );

        if (config == null)
        {
            return Response.Failed($"Config with ID '{id}' not found.");
        }

        config.ModifiedBy = modifiedBy;
        repository.Delete(config);
        await _unitOfWork.SaveChangesAsync();

        return Response.Success("Config deleted successfully.");
    }

    private static ConfigResponse MapToResponse(Config config)
    {
        return new ConfigResponse
        {
            Id = config.Id,
            Name = config.Name,
            Description = config.Description,
            VehicleId = config.VehicleId,
            VehicleName = config.Vehicle?.ModelName,
            CreatedAt = config.CreatedAt,
            CreatedBy = config.CreatedBy,
            ModifiedAt = config.ModifiedAt,
            ModifiedBy = config.ModifiedBy,
            IsActive = config.IsActive
        };
    }
}
