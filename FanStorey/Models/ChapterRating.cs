using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanStorey.Models
{
    public class ChapterRating
    {
        public int Id { get; set; }
        public IdentityUser Rater { get; set; }
        public Chapter RatedChapter { get; set; }
        public int Rating { get; set; }

        public ChapterRating() {}
    }
}
