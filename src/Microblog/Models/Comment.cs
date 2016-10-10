using System;
using System.ComponentModel.DataAnnotations;

namespace Microblog.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [Display(Name = "Date")]
        public DateTime PostDate { get; set; }
        public string Content { get; set; }

        [Display(Name = "By")]
        public ApplicationUser User { get; set; }
        public Post post { get; set; }
    }
}
