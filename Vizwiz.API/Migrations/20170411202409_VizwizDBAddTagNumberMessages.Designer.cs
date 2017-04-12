using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Vizwiz.API.Entities;

namespace Vizwiz.API.Migrations
{
    [DbContext(typeof(VizwizContext))]
    [Migration("20170411202409_VizwizDBAddTagNumberMessages")]
    partial class VizwizDBAddTagNumberMessages
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Vizwiz.API.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("TagId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Vizwiz.API.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NumberMessages");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Vizwiz.API.Entities.Message", b =>
                {
                    b.HasOne("Vizwiz.API.Entities.Tag", "Tag")
                        .WithMany("Messages")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
