using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementAPI_NoJWT.Models;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=localhost;Database=StudentDataBase;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC07C3BDC87F");

            entity.Property(e => e.Grade).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
