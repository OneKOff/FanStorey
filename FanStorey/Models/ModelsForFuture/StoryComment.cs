using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanStorey.Models
{
    public class StoryComment
    {
        public int Id { get; set; }
        public IdentityUser Commenter { get; set; }
        public Story CommentedStory { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        public StoryComment() {}
    }
}
