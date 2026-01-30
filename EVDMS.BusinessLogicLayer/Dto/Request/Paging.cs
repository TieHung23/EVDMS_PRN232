using System.Text.Json.Serialization;

namespace EVDMS.BusinessLogicLayer.Dto.Request;

public abstract class Paging
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;

    [JsonIgnore]
    public int Skip => (PageNumber - 1) * PageSize;

    [JsonIgnore]
    public int Take => PageSize;

    protected Paging()
    {
    }

    protected Paging(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}