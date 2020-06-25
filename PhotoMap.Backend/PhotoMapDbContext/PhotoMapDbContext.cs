﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using PhotoMap.Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace PhotoMap
{
    public partial class PhotoMapDbContext : DbContext
    {
        public PhotoMapDbContext()
        {
        }

        public PhotoMapDbContext(DbContextOptions<PhotoMapDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(e => e.PhotoRowguid)
                    .HasName("PK__Photo__5A3C7C4FE2F4506A");

                entity.ToTable("Photo");

                entity.Property(e => e.PhotoRowguid)
                    .HasColumnName("PhotoROWGUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasMaxLength(20);

                entity.Property(e => e.Longitude).HasMaxLength(20);

                entity.Property(e => e.PhotoPath).HasMaxLength(200);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UserRowguid).HasColumnName("UserROWGUID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.UserRowguid)
                    .HasConstraintName("FK__Photo__UserROWGU__4BAC3F29");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserRowguid)
                    .HasName("PK__User__AB6027480C6B95D9");

                entity.ToTable("User");

                entity.Property(e => e.UserRowguid)
                    .HasColumnName("UserROWGUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Login).HasMaxLength(30);

                entity.Property(e => e.Password).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}