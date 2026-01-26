using EVDMS.BusinessLogicLayer.Dto.Request;

namespace EVDMS.BusinessLogicLayer.Dto.Response.Dealer;

public class DealerDTO
{
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }
}


public class DealerResponse : Paging
{
    public List<DealerDTO> Dealers { get; set; } = new List<DealerDTO>();
}