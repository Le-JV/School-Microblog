using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Microblog.Models.HomeViewModels
{
    // Lonely view model :(
    public class HomeViewModel
    {
        public string Title { get; set; }
        public SelectList InterestsList { get; set; }
        public List<Post> PostsList { get; set; }
        public Post EmptyPost { get; set; }
        public string SearchTerm { get; set; }
    }
}
