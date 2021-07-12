using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanStorey.Models
{
    public class StoryRating
    {
        public int Id { get; set; }
        public int Value { get; set; }

        public string RaterId { get; set; }
        public ApplicationUser Rater { get; set; }
        public int StoryId { get; set; }
        public Story Story { get; set; }

        public StoryRating() {}
    }
}
