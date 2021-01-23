using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Projekt.NET_CORE.wwwroot.Model;

namespace Projekt.NET_CORE.Pages.ViewTraces
{
    public class AddModel : PageModel
    {
        private readonly ApplicationDbContext _database;
        private readonly IConfiguration _config;
        public Trace Trace { set; get; }

        public AddModel(ApplicationDbContext db, IConfiguration config)
        {
            _database = db;
            _config = config;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(Trace trace, IFormFile file)
        {
            int ID;
            Trace lastTrace;
            if(_database.Trace.Count()==0)
            {
                ID = 1;
            }
            else
            {
                lastTrace = _database.Trace.Last();
                ID = lastTrace.Id + 1;
            }
            
            trace.UserId = 1;
            string pathToFile = _config.GetValue<string>("FTP:adress") + ID + "_" + trace.UserId + "_" + file.FileName;
            trace.TracePoints = pathToFile;

            if (ModelState.IsValid)
            {
                await _database.Trace.AddAsync(trace);
                await _database.SaveChangesAsync();

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(pathToFile);
                request.Credentials = new NetworkCredential(_config.GetValue<string>("FTP:username"), _config.GetValue<string>("FTP:password"));
                request.Method = WebRequestMethods.Ftp.UploadFile;

                using (Stream ftpStream = request.GetRequestStream())
                {
                    file.CopyTo(ftpStream);
                }

                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
