using System.Net;

namespace StudentDocumentManagement.Core.Application.Shared.Results;

public class Result
{
    public string Message { get; set; }
    public bool Success { get; set; }

    public Result()
    {
        Message = string.Empty;
        Success = true;
    }

    public Result(bool success)
    {
        Message = string.Empty;
        Success = success;
    }

    public Result(bool success, string message)
    {
        Message = message;
        Success = success;
    }
}