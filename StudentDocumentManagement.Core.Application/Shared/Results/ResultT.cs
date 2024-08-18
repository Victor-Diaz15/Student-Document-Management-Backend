using System.Net;

namespace StudentDocumentManagement.Core.Application.Shared.Results;

public class ResultT<TValue> : Result
{
    public TValue? Data { get; set; }

    public ResultT(TValue value)
        :base() => Data = value;

    public ResultT(bool success, TValue value)
        : base(success) => Data = value;

    public ResultT(bool success, string message, TValue value)
        : base(success, message) => Data = value;
}

