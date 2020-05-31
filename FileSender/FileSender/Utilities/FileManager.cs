using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace FileSender.Utilities
{
    public class FileManager
    {
        public string SaveFile(IFormFile Upload, string uploadDirectory)
        {
            //atribui um novo nome aleatório ao arquivo
            var newFileName = Guid.NewGuid().ToString().Substring(0, 10);

            //pega a extensão do arquivo a partir da última ocorrência do .
            //subtraindo com o tamanho da string
            int startIndex = Upload.FileName.LastIndexOf('.');
            int endIndex = Upload.FileName.Length;
            int lenght = endIndex - startIndex;

            var fileType = Upload.FileName.Substring(startIndex, lenght);

            //verifica se existe algum arquivo com o mesmo nome
            //caso haja gera um novo nome
            while (File.Exists(Path.Combine(uploadDirectory, newFileName + fileType)))
            {
                newFileName = Guid.NewGuid().ToString().Substring(0, 10);
            }

            //salva a imagem no diretório informado
            //através de stream de dados
            var filePath = Path.Combine(uploadDirectory, newFileName + fileType);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Upload.CopyTo(fileStream);
            }

            return filePath;
        }
    }
}
