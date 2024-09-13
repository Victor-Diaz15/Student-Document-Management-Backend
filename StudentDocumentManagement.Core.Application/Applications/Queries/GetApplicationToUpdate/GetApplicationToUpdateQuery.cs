using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Applications.Queries.GetApplicationToUpdate;

public sealed record GetApplicationToUpdateQuery(Guid ApplicationId) : IQuery<ApplicationDto>;
