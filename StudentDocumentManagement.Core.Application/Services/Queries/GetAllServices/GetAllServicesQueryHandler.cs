using StudentDocumentManagement.Core.Application.Dtos.Services;
using StudentDocumentManagement.Core.Application.Helpers;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Core.Domain.Entities;

namespace StudentDocumentManagement.Core.Application.Services.Queries.GetAllServices;

internal class GetAllServicesQueryHandler : IQueryHandler<GetAllServicesQuery, List<ServiceDto>>
{
    private readonly IGenericBaseRepository<Service> _serviceRepository;

    public GetAllServicesQueryHandler(IGenericBaseRepository<Service> serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<ResultT<List<ServiceDto>>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
        var serviceListDb = await _serviceRepository.GetAllAsync();
        var listDto = new List<ServiceDto>();

        if (serviceListDb != null && serviceListDb.Count != 0)
        {
            foreach (var service in serviceListDb)
            {
                listDto.Add(new ServiceDto()
                {
                    Id = service.Id,
                    Name = service.Name,
                    Price = service.Price,
                    ServiceType = ServiceTypeHelper.GetServiceType(service.ServiceType)
                });
            }

            return new ResultT<List<ServiceDto>>(true, "Retrieving services of the system.", listDto);
        }

        return new ResultT<List<ServiceDto>>(false, "There are not services in the system.", null!);
    }
}
