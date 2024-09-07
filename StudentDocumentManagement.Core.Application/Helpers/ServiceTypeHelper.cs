using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Application.Helpers;

public static class ServiceTypeHelper
{
    public static string GetServiceType(ServiceType serviceType)
    {
        string type = serviceType switch
        {
            ServiceType.MESCYT => ServiceType.MESCYT.ToString(),
            ServiceType.PERSONAL => ServiceType.PERSONAL.ToString(),
            _ => ""
        };

        return type;
    }
}
