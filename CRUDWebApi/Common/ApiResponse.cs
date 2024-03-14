namespace CRUDWebApi.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }

    public ApiResponse(bool success, T data = default, string errorMessage = null, int statusCode = 0)
    {
        Success = success;
        Data = data;
        Message = errorMessage;
        StatusCode = statusCode;
    }
}



public class ApiPaginationResponse<T>
{
    public bool Success { get; set; }
    public List<T> Data { get; set; }
    public string Message { get; set; }
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
    public int TotalPage { get; set; }
    public int StatusCode { get; set; }

    public ApiPaginationResponse(bool success, List<T> data = default, string errorMessage = null, int pageNumber = 0, int pageSize = 0, int totalPage = 0, int statusCode = 0)
    {
        Success = success;
        Data = data;
        Message = errorMessage;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPage = totalPage;
        StatusCode = statusCode;
    }
}