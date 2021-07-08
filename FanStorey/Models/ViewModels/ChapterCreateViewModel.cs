using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanStorey.Models.ViewModels
{
    public class ChapterCreateViewModel
    {
        public Chapter ChapterNew { get; set; }
        public int? StoryId { get; set; }
    }
}
