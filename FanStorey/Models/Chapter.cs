using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanStorey.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        [Display(Name = "Chapter Title")]
        public string Title { get; set; }
        [Display(Name = "Chapter Text"), UIHint("MultilineText"), DataType(DataType.MultilineText)]
        public string ChapterText { get; set; }
        [Display(Name = "Posting Date"), DataType(DataType.DateTime)]
        public DateTime PostDate { get; set; }
        [Display(Name = "Last Update Date"), DataType(DataType.DateTime)]
        public DateTime LastUpdateDate { get; set; }

        public int StoryId { get; set; }
        public Story Story { get; set; }
        
        public Chapter() {}
    }
}
