using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Microblog.Models.PostViewModels
{
    public class PostViewModel
    {
        [Required]
        [StringLength(65, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(10000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 144)]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        public bool Public { get; set; }

        public SelectList InterestsList { get; set; }

        [Required]
        public int[] InterestIds { get; set; }
    }
}
