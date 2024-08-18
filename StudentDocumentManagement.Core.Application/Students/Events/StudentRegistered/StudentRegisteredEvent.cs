using MediatR;

namespace StudentDocumentManagement.Core.Application.Students.Events.StudentRegistered;

public sealed record StudentRegisteredEvent(string Email, string Username, string Password) : INotification;
