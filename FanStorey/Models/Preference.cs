using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanStorey.Models
{
    public class Preference
    {
        public int Id { get; set; }
        public Fandom PrefFandom { get; set; }

        public Preference() {}
    }
}
