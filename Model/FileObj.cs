using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.NET_CORE.Model
{
    public class FileObj
    {
        private IFormFile file;
        private string fileName;

        public FileObj(IFormFile f, string s)
        {
            file = f;
            fileName = s;
        }

        public IFormFile getFile()
        {
            return file;
        }

        public string getName()
        {
            return fileName;
        }
    }
}
