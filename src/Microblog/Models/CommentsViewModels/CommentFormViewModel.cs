using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Microblog.Models.CommentsViewModels
{
    // Lonely view model :(
    public class CommentFormViewModel
    { 
        [Required]
        public string Content { get; set; }
    }
}
