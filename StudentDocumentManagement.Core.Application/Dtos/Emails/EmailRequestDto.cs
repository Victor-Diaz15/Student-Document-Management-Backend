namespace StudentDocumentManagement.Core.Application.Dtos.Emails;

public sealed record EmailRequestDto(string To, string Subject, string Body);
