using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

public partial class StudentDataBaseContext : DbContext
{
    public StudentDataBaseContext()
    {
    }

    public StudentDataBaseContext(DbContextOptions<StudentDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=localhost;Database=StudentDataBase;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52B99B939794D");

            entity.ToTable("Student");

            entity.Property(e => e.Grade).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

      
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC079C671F22");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E46B588ACC").IsUnique();

            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
