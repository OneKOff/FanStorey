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
        public IdentityUser Rater { get; set; }
        public Story RatedStory { get; set; }
        public int Rating { get; set; }

        public StoryRating() {}
    }
}
