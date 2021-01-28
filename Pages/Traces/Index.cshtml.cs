using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
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
        private object blokowacz = new object();
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
            Object path = (Object)trace;
            Thread watek = new Thread(deleteFile);
            watek.Start(path);
            _database.Trace.Remove(trace);
            await _database.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        public void deleteFile(Object o)
        {
            lock(blokowacz)
            {
                Trace trace = (Trace)o;
                try
                {
                    if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/GPX", trace.TracePoints)))
                    {
                        System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/GPX", trace.TracePoints));
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
