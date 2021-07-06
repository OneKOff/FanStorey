using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanStorey.Models
{
    public class Story
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        [UIHint("MultilineText")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        public List<Chapter> Chapters { get; set; }

        [Display(Name = "Story Fandom")]
        public Fandom StoryFandom { get; set; }

        public IdentityUser Author { get; set; }

        [Display(Name = "Posting Date")]
        [DataType(DataType.DateTime)]
        public DateTime PostDate { get; set; }

        public Story() {}
    }
}
