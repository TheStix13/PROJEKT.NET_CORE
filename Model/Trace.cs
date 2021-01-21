using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.NET_CORE.wwwroot.Model
{
    public class Trace
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TraceName { get; set; }

        [Required]
        public string TraceStart { get; set; }

        [Required]
        public string TraceEnd { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string TracePoints { get; set; }
    }
}
