using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Projekt.NET_CORE.wwwroot.Model;

namespace Projekt.NET_CORE.Pages.ViewTraces
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _database;
        private readonly IConfiguration _config;
        public IndexModel(ApplicationDbContext db, IConfiguration config)
        {
            _database = db;
            _config = config;
        }

        public IEnumerable<Trace> Traces { set; get; }
        public async Task OnGet()
        {
            Traces = await _database.Trace.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var trace = await _database.Trace.FindAsync(id);
            if(trace == null)
            {
                return NotFound();
            }

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(trace.TracePoints);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(_config.GetValue<string>("FTP:username"), _config.GetValue<string>("FTP:password"));

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(response.StatusDescription);
            }
            _database.Trace.Remove(trace);
            await _database.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
