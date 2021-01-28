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
                lastTrace = _database.Trace.OrderBy(item=> item.Id).Last();
                ID = lastTrace.Id + 1;
            }
            
            

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string GPXName = ID+"_"+trace.Id+"_"+file.FileName;
                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/GPX", GPXName);
                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    string filename = GPXName;
                    trace.UserId = 1;
                    trace.TracePoints = filename;

                    await _database.Trace.AddAsync(trace);
                    await _database.SaveChangesAsync();

                    /*
                     * Dla po³¹czenia z FTP
                     * 
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(pathToFile);
                    request.Credentials = new NetworkCredential(_config.GetValue<string>("FTP:username"), _config.GetValue<string>("FTP:password"));
                    request.Method = WebRequestMethods.Ftp.UploadFile;

                    using (Stream ftpStream = request.GetRequestStream())
                    {
                        file.CopyTo(ftpStream);
                    }
                    */
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
