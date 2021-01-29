using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Projekt.NET_CORE.Model;
using Projekt.NET_CORE.wwwroot.Model;

namespace Projekt.NET_CORE.Pages.ViewTraces
{
    public class AddModel : PageModel
    {
        private readonly ApplicationDbContext _database;
        private readonly IConfiguration _config;
        private object blokowacz = new object();
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

                    FileObj fileObject = new FileObj(file, GPXName);
                    Object paramObj = (Object)fileObject;
                    Thread watek = new Thread(saveFile);
                    watek.Start(paramObj);

                    trace.UserId = 1;
                    trace.TracePoints = GPXName;

                    await _database.Trace.AddAsync(trace);
                    await _database.SaveChangesAsync();
                }
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

        public void saveFile(object obj)
        {
            FileObj file = (FileObj)obj;

            lock(blokowacz)
            {
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/GPX", file.getName());
                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    file.getFile().CopyTo(stream);
                }
            }    
        }
    }
}
