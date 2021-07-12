using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        [UIHint("MultilineText"), DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Posting Date"), DataType(DataType.DateTime)]
        public DateTime PostDate { get; set; }
        [Display(Name = "Last Update Date"), DataType(DataType.DateTime)]
        public DateTime LastUpdateDate { get; set; }

        public List<Chapter> Chapters { get; set; }        

        public int FandomId { get; set; }
        public Fandom Fandom { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Story() {}
    }
}