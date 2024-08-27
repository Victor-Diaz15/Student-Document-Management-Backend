using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using StudentDocumentManagement.Core.Application.Interfaces;

namespace StudentDocumentManagement.Infrastructure.Services;

public class StorageFileLocalService : IStorageFile
{
    private readonly IWebHostEnvironment env;
    private readonly IHttpContextAccessor httpContextAccessor;

    public StorageFileLocalService(IWebHostEnvironment env,
        IHttpContextAccessor httpContextAccessor)
    {
        this.env = env;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> SaveFile(byte[] content, string extension, string contenedor, string contentType)
    {
        var nombreArchivo = $"{Guid.NewGuid()}{extension}";
        string folder = Path.Combine(env.WebRootPath, contenedor);

        //si el directorio no exite construirlo
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        string ruta = Path.Combine(folder, nombreArchivo);
        await File.WriteAllBytesAsync(ruta, content);

        var urlActual = $"{httpContextAccessor?.HttpContext?.Request.Scheme}://{httpContextAccessor?.HttpContext?.Request.Host}";
        var urlParaDb = Path.Combine(urlActual, contenedor, nombreArchivo).Replace("\\", "/");
        return urlParaDb;
    }

    public async Task<string> UpdateFile(byte[] content, string extension, string contenedor, string contentType, string ruta)
    {
        await DeleteFile(ruta, contenedor);
        return await SaveFile(content, extension, contenedor, contentType);
    }

    public Task DeleteFile(string ruta, string contenedor)
    {
        if (ruta != null)
        {
            var nombreArchivo = Path.GetFileName(ruta);
            string directorioArchivo = Path.Combine(env.WebRootPath, contenedor, nombreArchivo);

            if (File.Exists(directorioArchivo))
            {
                File.Delete(directorioArchivo);
            }
        }

        return Task.FromResult(0);
    }
}
