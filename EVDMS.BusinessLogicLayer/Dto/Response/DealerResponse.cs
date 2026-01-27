using EVDMS.Core.Model;

namespace EVDMS.BusinessLogicLayer.Dto.Response;

public class DealerResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public static DealerResponse FromEntity(Dealer dealer)
    {
        return new DealerResponse
        {
            Id = dealer.Id,
            Code = dealer.Code,
            Name = dealer.Name,
            Email = dealer.Email,
            IsActive = dealer.IsActive,
            IsDeleted = dealer.IsDeleted,
            CreatedAt = dealer.CreatedAt,
            ModifiedAt = dealer.ModifiedAt
        };
    }
}
