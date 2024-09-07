using AutoMapper;
using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Domain.Entities;

namespace StudentDocumentManagement.Infrastructure.Mappers.Applications;

public class ApplicationStudentFileMapper : Profile
{
    public ApplicationStudentFileMapper()
    {
        CreateMap<ApplicationStudentFile, ApplicationStudentFileDto>()
            .ForMember(x => x.Url, m => m.MapFrom(f => f.StudentFile!.Url))
            .ForMember(x => x.FileType, m => m.MapFrom(f => f.StudentFile!.FileType))
            .ReverseMap();
        
        CreateMap<ApplicationStudentFile, ApplicationStudentFileCreateDto>()
            .ReverseMap();
    }
}
