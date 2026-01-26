using EVDMS.BusinessLogicLayer.Dto.Request;

namespace EVDMS.BusinessLogicLayer.Helper;

public static class RequestExtensions
{
    public static (int skip, int take) ToSkipTake(int pageNumber, int pageSize)
    {
        var take = pageSize > 0 ? pageSize : 10;
        var skip = (pageNumber > 0 ? pageNumber - 1 : 0) * take;
        return (skip, take);
    }
}