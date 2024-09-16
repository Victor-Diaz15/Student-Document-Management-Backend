using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Application.Applications.Queries.GetApplicationByFilters;

public sealed record GetApplicationsByFiltersQuery(
    string? StudentId, 
    string? ApplicationNumberId, 
    string? ServiceId, 
    ApplicationStatus? Status) : IQuery<List<ApplicationDto>>;
