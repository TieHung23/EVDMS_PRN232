namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class Paging
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public int Skip => (PageNumber - 1) * PageSize;

    public int Take => PageSize;

    public Paging()
    {
    }

    public Paging(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}