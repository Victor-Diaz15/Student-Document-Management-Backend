using StudentDocumentManagement.Core.Application.Dtos.Services;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Services.Queries.GetAllServices;

public sealed record GetAllServicesQuery : IQuery<List<ServiceDto>>;
