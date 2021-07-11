using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FanStorey.Models.ViewModels
{
    public class UserPageViewModel
    {
        public ApplicationUser User { get; set; }
        [Display(Name = "User Stories")]
        public List<Story> UserStories { get; set; }
        public bool YourAccount { get; set; }

        public UserPageViewModel(ApplicationUser User, List<Story> UserStories, bool YourAccount)
        {
            this.User = User;
            this.UserStories = UserStories;
            this.YourAccount = YourAccount;
        }
    }
}
