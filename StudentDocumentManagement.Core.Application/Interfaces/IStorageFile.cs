namespace StudentDocumentManagement.Core.Application.Interfaces;

public interface IStorageFile
{
    Task<string> SaveFile(byte[] content, string extension, string contenedor, string contentType);

    Task<string> UpdateFile(byte[] content, string extension, string contenedor, string contentType,
        string ruta);

    Task DeleteFile(string ruta, string contenedor);
}
