using AutoMapper;
using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Domain.Entities;
using System.Text.RegularExpressions;

namespace StudentDocumentManagement.Infrastructure.Mappers.Applications;

public class ApplicationStudentFileMapper : Profile
{
    public ApplicationStudentFileMapper()
    {
        CreateMap<ApplicationStudentFile, ApplicationStudentFileDto>()
            .ForMember(x => x.Url, m => m.MapFrom(f => f.StudentFile!.Url))
            .ForMember(x => x.FileType, m => m.MapFrom(f => Regex.Replace(f.StudentFile!.FileType.ToString()!, "(?<!^)([A-Z])", " $1")))
            .ForMember(x => x.Status, m => m.MapFrom(f => f.StudentFile!.Status))
            .ReverseMap();
        
        CreateMap<ApplicationStudentFile, ApplicationStudentFileCreateDto>()
            .ReverseMap();
    }
}
