using System;
using System.ComponentModel.DataAnnotations;

namespace Microblog.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [Display(Name = "Date")]
        public DateTime PostDate { get; set; }

        [Required]
        public string Content { get; set; }

        [Display(Name = "By")]
        public ApplicationUser User { get; set; }
        public Post Post { get; set; }
    }
}
