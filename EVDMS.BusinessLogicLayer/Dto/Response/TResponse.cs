using System.Text.Json.Serialization;

namespace EVDMS.BusinessLogicLayer.Dto.Response;

public class TResponse<T> : Response
{
    [JsonPropertyOrder(4)]
    public T? Data { get; set; }


    public TResponse(T data,bool isSuccess, bool isFailed, string message) : base(isSuccess, isFailed, message)
    {
        Data = data;
    }

    public static TResponse<T> Success(T data, string message = "Operation completed successfully.")
    {
        return new TResponse<T>(data,true, false, message);
    }

    public new static TResponse<T> Failed(string message = "Operation failed.")
    {
        return new TResponse<T>(default!,false, true, message);
    }
}