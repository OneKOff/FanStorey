using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanStorey.Models
{
    public class Fandom
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Story> Stories { get; set; }

        public Fandom() {}
    }
}
