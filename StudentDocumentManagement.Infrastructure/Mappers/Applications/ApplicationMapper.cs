using AutoMapper;
using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Domain.Entities;

namespace StudentDocumentManagement.Infrastructure.Mappers.Applications;

internal class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {
        CreateMap<Application, ApplicationDto>()
            .ForMember(x => x.ServiceType, m => m.MapFrom(a => a.Service!.ServiceType))
            .ReverseMap();
    }
}
