
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Domain.Enums;
using System.Text.Json.Serialization;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Commands.ChangeStatus;

public sealed record ChangeStatusStudentFileCommand(FileStatus Status) : ICommand
{
    [JsonIgnore]
    public Guid FileId { get; set; }
}
