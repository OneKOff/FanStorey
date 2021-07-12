using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanStorey.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Registered")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "Last Visited")]
        public DateTime LastVisitDate { get; set; }    
        public bool Admin { get; set; } = false;
        public bool Blocked { get; set; } = false;

        public List<Story> Stories { get; set; }
    }
}
