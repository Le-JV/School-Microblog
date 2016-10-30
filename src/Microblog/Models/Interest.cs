using Microblog.Data;
using System.Collections.Generic;

namespace Microblog.Models
{
    public class Interest
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<UserInterests> UserInterests { get; set; }
        public ICollection<PostInterests> PostInterests { get; set; }
    }
}
