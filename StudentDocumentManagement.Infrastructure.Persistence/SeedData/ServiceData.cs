using Microsoft.EntityFrameworkCore;
using StudentDocumentManagement.Core.Domain.Entities;
using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Domain.SeedData;

public static class ServiceData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>()
            .HasData(new List<Service>
            {
                new()
                {
                    Id = new Guid("21f6fed9-9fa6-4476-9f22-7d630fb3cec7"),
                    Name = "Legalización Completa",
                    ServiceType = ServiceType.MESCYT,
                    Price = 650,
                    Borrado = false,
                },
                new()
                {
                    Id = new Guid("197e5630-d2d0-4699-845a-11a78d25a569"),
                    Name = "Copia Título Legalizada",
                    ServiceType = ServiceType.MESCYT,
                    Price = 150,
                    Borrado = false,
                },
                new()
                {
                    Id = new Guid("53b6a489-b2e4-4ec4-a248-19e7f3999a29"),
                    Name = "Récord de Nota Legalizada",
                    ServiceType = ServiceType.MESCYT,
                    Price = 200,
                    Borrado = false,
                },
                new()
                {
                    Id = new Guid("25a9a1fe-b559-4168-b6bc-97f677721397"),
                    Name = "Certificación Título Legalizada",
                    ServiceType = ServiceType.MESCYT,
                    Price = 150,
                    Borrado = false,
                },
                new()
                {
                    Id = new Guid("bad159b3-1582-45a3-a4b9-1bbe736770dd"),
                    Name = "Carta de Doctorado Especial Legalizada",
                    ServiceType = ServiceType.MESCYT,
                    Price = 250,
                    Borrado = false,
                },
                new()
                {
                    Id = new Guid("26214a95-18c5-4f18-b936-bbe7cfe72d46"),
                    Name = "Legalización Completa",
                    ServiceType = ServiceType.PERSONAL,
                    Price = 650,
                    Borrado = false,
                },
                new()
                {
                    Id = new Guid("1b06e340-5eee-427d-b0a7-063e17464dc1"),
                    Name = "Copia Título Legalizada",
                    ServiceType = ServiceType.PERSONAL,
                    Price = 150,
                    Borrado = false,
                },
                new()
                {
                    Id = new Guid("4999cfc4-661b-48ed-bc60-41f07a855a36"),
                    Name = "Récord de Nota Legalizada",
                    ServiceType = ServiceType.PERSONAL,
                    Price = 200,
                    Borrado = false,
                },
                new()
                {
                    Id = new Guid("8d11c889-679f-4d38-854e-9755df726632"),
                    Name = "Certificación Título Legalizada",
                    ServiceType = ServiceType.PERSONAL,
                    Price = 150,
                    Borrado = false,
                },
                new()
                {
                    Id = new Guid("d41173e3-cf37-48dd-898a-9bb6bf65a54a"),
                    Name = "Carta de Doctorado Especial Legalizada",
                    ServiceType = ServiceType.PERSONAL,
                    Price = 250,
                    Borrado = false,
                }
            });
    }
}
