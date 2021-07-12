using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanStorey.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string CommentText { get; set; }

        public string CommenterId { get; set; }
        public ApplicationUser Commenter { get; set; }
        public int StoryId { get; set; }
        public Story Story { get; set; }

        public Comment() {}
    }
}
