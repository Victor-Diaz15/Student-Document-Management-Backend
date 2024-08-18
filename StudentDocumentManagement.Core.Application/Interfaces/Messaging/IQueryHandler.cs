using MediatR;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Interfaces.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, ResultT<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
