using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : BaseStorage, ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            //Upload konumu var mı?
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string path)> filesList = new();

            foreach (var file in files)
            {
                string newFileName = await RenameFileAsync(file.Name); 

                await CopyFileAsync($"{uploadPath}/{newFileName}", file);
                filesList.Add((newFileName, $"{path}/{newFileName}"));
            }



            return filesList;
            //todo Exception H.
        }

        public async Task DeleteAsync(string path, string fileName) => File.Delete($"{path}\\{fileName}");

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName) => File.Exists($"{path}\\{fileName}");


        private async Task<bool> CopyFileAsync(string path, IFormFile file)
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
