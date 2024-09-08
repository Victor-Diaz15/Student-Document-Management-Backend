using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Applications.Queries.GetApplicationById;

public sealed record GetApplicationByIdQuery(Guid ApplicationId) : IQuery<ApplicationDto>;
