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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTag>()
                .HasKey(t => new { t.MessageId, t.TagId });

            modelBuilder.Entity<MessageTag>()
                .HasOne(mt => mt.Message)
                .WithMany(m => m.MessageTags)
                .HasForeignKey(mt => mt.MessageId);

            modelBuilder.Entity<MessageTag>()
                .HasOne(mt => mt.Tag)
                .WithMany(t => t.MessageTags)
                .HasForeignKey(mt => mt.TagId);

        }
    }
}
