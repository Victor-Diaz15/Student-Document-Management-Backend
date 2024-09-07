using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Applications.Queries.GetAllApplications;

public sealed record GetAllApplicationsQuery() : IQuery<List<ApplicationDto>>;
