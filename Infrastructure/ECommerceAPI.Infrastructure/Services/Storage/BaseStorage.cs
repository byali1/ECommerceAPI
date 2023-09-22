using ECommerceAPI.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Storage
{
    public class BaseStorage
    {

        protected async Task<string> RenameFileAsync(string fileName)
        {

            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string fixedName = NameOperation.CharacterConverter(oldName);

            string newFileName = string.Concat(fixedName, "_", DateTime.Now.ToString("ddMMyyyy"), "_", Guid.NewGuid(), extension);

            return newFileName;
        }
       
    }
}
