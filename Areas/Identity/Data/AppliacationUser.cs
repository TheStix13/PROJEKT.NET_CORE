using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Projekt.NET_CORE.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AppliacationUser class
    public class AppliacationUser : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nick { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
