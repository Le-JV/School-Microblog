using System;
using System.Collections.Generic;
using System.Linq;
using Microblog.Data;
using Microblog.Models;

namespace Microblog.Helpers
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            
            // Has the database been seeded already?
            if (context.Interest.Any())
            {
                return;
            }

            // Seed the database with the list of standard interests.
            var interests = new Interest[]
            {
                new Interest {Name="Leisure"},
                new Interest {Name="Music"},
                new Interest {Name="Movies"},
                new Interest {Name="TV"},
                new Interest {Name="Gaming"},
                new Interest {Name="Technology"},
            };

            foreach(Interest i in interests)
            {
                context.Interest.Add(i);
            }

            context.SaveChanges();
        }
    }
}