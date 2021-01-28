using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Projekt.NET_CORE.wwwroot.Model;

namespace Projekt.NET_CORE.Pages
{
    public class OpenStreetMapModel : PageModel
    {
        private readonly ApplicationDbContext _database;
        private readonly IConfiguration _config;
        private readonly string webPath;
        public OpenStreetMapModel(ApplicationDbContext db, IConfiguration config)
        {
            _database = db;
            _config = config;
            webPath = config.GetValue<string>("GPX:adress");
        }

        public string getWebPath()
        {
            return webPath;
        }
        [BindProperty]
        public Trace trace { set; get; }
        public async Task OnGet(int id)
        {
            trace = await _database.Trace.FindAsync(id);
        }
    }
}