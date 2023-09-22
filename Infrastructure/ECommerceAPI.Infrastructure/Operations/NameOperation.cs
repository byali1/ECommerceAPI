using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Operations
{
    public class NameOperation
    {
        public static string CharacterConverter(string name)
        {
            string source = @"ığüşöçĞÜŞİÖÇâß\!'^+%&/()=?_@€¨~,;:<>|. ";
            string destination = @"igusocGUSIOC                          __";

            for (int i = 0; i < source.Length; i++)
            {
                name = name.Replace(source[i], destination[i]);
            }
            return name;
        }
    }
}
