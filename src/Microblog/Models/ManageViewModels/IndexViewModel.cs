using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microblog.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Microblog.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public ICollection<UserInterests> Interests { get; set; }
        public SelectList InterestsList { get; set; }
    }
}
