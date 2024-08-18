using MediatR;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<ResultT<TResponse>>
{
}
