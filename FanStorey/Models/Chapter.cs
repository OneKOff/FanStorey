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
        public string ChapterTitle { get; set; }

        [Display(Name = "Chapter Text")]
        [UIHint("MultilineText")]
        [DataType(DataType.MultilineText)]
        public string ChapterText { get; set; }

        [Display(Name = "Image Path")]
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }

        [Display(Name = "Posting Date")]
        [DataType(DataType.DateTime)]
        public DateTime PostDate { get; set; }
        
        public Chapter() {}
    }
}
