﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Microblog.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [Display(Name = "Date")]
        public DateTime PostDate { get; set; }

        public string Content { get; set; }
        public string Excerpt { get; set; }
        public bool Public { get; set; }

        [Display(Name = "By")]
        public ApplicationUser User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
