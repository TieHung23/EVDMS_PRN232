namespace EVDMS.BusinessLogicLayer.Dto.Response;

public class Response
{

    public bool IsSuccess { get; set; }

    public bool IsFailed { get; set; }

    public string Message { get; set; } = string.Empty;

    private Response()
    {

    }

    protected Response(bool isSuccess, bool isFailed, string message)
    {
        if (isSuccess && isFailed)
        {
            throw new ArgumentException("Response cannot be both success and failed.");
        }

        IsSuccess = isSuccess;
        IsFailed = isFailed;
        Message = message;
    }

    public static Response Success(string message = "Operation completed successfully.")
    {
        return new Response(true, false, message);
    }

    public static Response Failed(string message = "Operation failed.")
    {
        return new Response(false, true, message);
    }
}