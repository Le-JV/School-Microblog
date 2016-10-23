using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Microblog.Models
{
    // Probably should make a viewmodel for displaying this.
    public class Post
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name = "Date")]
        public DateTime PostDate { get; set; }

        [Required]
        public string Content { get; set; }
        public string Excerpt { get; set; }

        [Required]
        public bool Public { get; set; }

        [Display(Name = "By")]
        public ApplicationUser User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Interest> Interests { get; set; }
    }
}
