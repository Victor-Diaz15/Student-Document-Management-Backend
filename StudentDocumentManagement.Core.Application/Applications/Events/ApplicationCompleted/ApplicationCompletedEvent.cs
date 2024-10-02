using MediatR;

namespace StudentDocumentManagement.Core.Application.Applications.Events.ApplicationCompleted;

public sealed record ApplicationCompletedEvent(List<Guid> ApplicationIds, string ApplicationNumberId) : INotification;
