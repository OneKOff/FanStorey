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
        public int Value { get; set; }

        public string RaterId { get; set; }
        public ApplicationUser Rater { get; set; }
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }

        public ChapterRating() {}
    }
}
