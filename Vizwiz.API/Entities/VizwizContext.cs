using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vizwiz.API.Entities
{
    public class VizwizContext: DbContext
    {
        public VizwizContext(DbContextOptions<VizwizContext> options)
            :base(options)
        {
            Database.Migrate();
        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
