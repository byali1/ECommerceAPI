using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services
{


    internal class FileService
    {

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

    }
}
