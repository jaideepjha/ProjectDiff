using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectDiff.Entities;

namespace ProjectDiff.Migrations
{
    [DbContext(typeof(ProjDiffDBContext))]
    partial class ProjDiffDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("ProjectDiff.Entities.FileProperty", b =>
                {
                    b.Property<int>("FilePropertyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("ProjectID");

                    b.Property<int>("SnapshotID");

                    b.Property<string>("TfsPath");

                    b.Property<string>("checksum");

                    b.Property<string>("path");

                    b.Property<string>("project");

                    b.HasKey("FilePropertyID");

                    b.HasIndex("ProjectID");

                    b.ToTable("FileProperties");
                });

            modelBuilder.Entity("ProjectDiff.Entities.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("SnapshotID");

                    b.Property<int>("SolutionID");

                    b.HasKey("ProjectID");

                    b.HasIndex("SolutionID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectDiff.Entities.Snapshot", b =>
                {
                    b.Property<int>("SnapshotID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SolutionID");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("SnapshotID");

                    b.ToTable("Snapshots");
                });

            modelBuilder.Entity("ProjectDiff.Entities.Solution", b =>
                {
                    b.Property<int>("SolutionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.HasKey("SolutionID");

                    b.ToTable("Solutions");
                });

            modelBuilder.Entity("ProjectDiff.Entities.FileProperty", b =>
                {
                    b.HasOne("ProjectDiff.Entities.Project")
                        .WithMany("ProjectFiles")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectDiff.Entities.Project", b =>
                {
                    b.HasOne("ProjectDiff.Entities.Solution")
                        .WithMany("Projects")
                        .HasForeignKey("SolutionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
