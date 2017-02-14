using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectDiff.Entities;

namespace ProjectDiff.Migrations
{
    [DbContext(typeof(ProjDiffDBContext))]
    [Migration("20170125114550_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("ProjectDiff.Entities.FileProperty", b =>
                {
                    b.Property<long>("FilePropertyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long>("ProjectID");

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
                    b.Property<long>("ProjectID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long>("SolutionID");

                    b.HasKey("ProjectID");

                    b.HasIndex("SolutionID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectDiff.Entities.Snapshot", b =>
                {
                    b.Property<long>("SnapshotID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("SolutionID");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("SnapshotID");

                    b.HasIndex("SolutionID");

                    b.ToTable("Snapshots");
                });

            modelBuilder.Entity("ProjectDiff.Entities.SnapshotFileProperty", b =>
                {
                    b.Property<long>("SnapshotID");

                    b.Property<long>("FilePropertyID");

                    b.HasKey("SnapshotID", "FilePropertyID");

                    b.HasIndex("FilePropertyID");

                    b.HasIndex("SnapshotID");

                    b.ToTable("SnapshotFileProperties");
                });

            modelBuilder.Entity("ProjectDiff.Entities.SnapshotProject", b =>
                {
                    b.Property<long>("SnapshotID");

                    b.Property<long>("ProjectID");

                    b.HasKey("SnapshotID", "ProjectID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("SnapshotID");

                    b.ToTable("SnapshotProjects");
                });

            modelBuilder.Entity("ProjectDiff.Entities.Solution", b =>
                {
                    b.Property<long>("SolutionID")
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

            modelBuilder.Entity("ProjectDiff.Entities.Snapshot", b =>
                {
                    b.HasOne("ProjectDiff.Entities.Solution")
                        .WithMany("Snapshots")
                        .HasForeignKey("SolutionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectDiff.Entities.SnapshotFileProperty", b =>
                {
                    b.HasOne("ProjectDiff.Entities.FileProperty", "FileProperty")
                        .WithMany("SnapshotFileProperties")
                        .HasForeignKey("FilePropertyID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectDiff.Entities.Snapshot", "Snapshot")
                        .WithMany("SnapshotFileProperties")
                        .HasForeignKey("SnapshotID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectDiff.Entities.SnapshotProject", b =>
                {
                    b.HasOne("ProjectDiff.Entities.Project", "Project")
                        .WithMany("SnapshotProjects")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectDiff.Entities.Snapshot", "Snapshot")
                        .WithMany("SnapshotProjects")
                        .HasForeignKey("SnapshotID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
