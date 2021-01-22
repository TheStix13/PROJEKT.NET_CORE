using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.NET_CORE.wwwroot.Model;

namespace Projekt.NET_CORE.Pages.ViewTraces
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _database;

        public IndexModel(ApplicationDbContext db)
        {
            _database = db;
        }

        public IEnumerable<Trace> Traces { set; get; }
        public async Task OnGet()
        {
            Traces = await _database.Trace.ToListAsync();
        }
    }
}
