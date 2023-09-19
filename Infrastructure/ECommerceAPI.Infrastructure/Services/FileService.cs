using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Services;
using ECommerceAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services
{


    internal class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            //Upload konumu var mı?
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string path)> filesList = new();

            List<bool> results = new();

            foreach (var file in files)
            {
                string newFileName = await RenameFileAsync(uploadPath, file.FileName);

                bool result = await CopyFileAsync($"{uploadPath}\\{newFileName}", file);
                filesList.Add((newFileName, $"{uploadPath}\\{newFileName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true)))
            {
                return filesList;
            }

            return null;
            //todo Exception H.

        }

        private async Task<string> RenameFileAsync(string path, string fileName)
        {
            int index = 0;
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string seaoFriendlyName = NameOperation.CharacterConverter(oldName);
            string newFileName = $"{NameOperation.CharacterConverter(seaoFriendlyName)}{extension}";

            while (true)
            {
                index++;
                if (File.Exists(Path.Combine(path, newFileName)))
                    newFileName = seaoFriendlyName + $"-{index}{extension}";
                else
                    break;
            }
            return newFileName;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {

            try
            {
                //Fonksiyon son bulunca 'using' sayesinde dispose edilecek.

                using FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write,
                   FileShare.None, 1024 * 1024,
                   useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception exception)
            {
                //todo log!
                throw exception;
            }
        }
    }
}
