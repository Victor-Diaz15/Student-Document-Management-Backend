using MediatR;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Events.ChangedFileStatus;

public sealed record FileStatusChangedEvent(Domain.Entities.Application Application) : INotification;
