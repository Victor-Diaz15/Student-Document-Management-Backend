using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Application.Helpers;

public static class StudentFileFuncionts
{
    public static string GetFileType(StudentFileType? type)
    {
        string fileType = type switch
        {
            StudentFileType.ActaNacimiento => StudentFileType.ActaNacimiento.ToString(),
            StudentFileType.CopiaCedula => StudentFileType.CopiaCedula.ToString(),
            StudentFileType.FotocopiaTituloMaestria => StudentFileType.FotocopiaTituloMaestria.ToString(),
            StudentFileType.DocumentoLegalizadoGrado => StudentFileType.DocumentoLegalizadoGrado.ToString(),
            _ => ""
        };

        return fileType;
    }

    public static string GetFileStatus(FileStatus? status)
    {
        string fileStatus = status switch
        {
            FileStatus.Nuevo => FileStatus.Nuevo.ToString(),
            FileStatus.Devuelto => FileStatus.Devuelto.ToString(),
            FileStatus.Validado => FileStatus.Validado.ToString(),
            FileStatus.Completado => FileStatus.Completado.ToString(),
            _ => ""
        };

        return fileStatus;
    }
}
