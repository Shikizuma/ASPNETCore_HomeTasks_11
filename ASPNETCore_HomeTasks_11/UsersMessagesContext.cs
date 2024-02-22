using System;
using System.Collections.Generic;
using ASPNETCore_HomeTasks_11.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore_HomeTasks_11;

public partial class UsersMessagesContext : DbContext
{
    public UsersMessagesContext()
    {
    }

    public UsersMessagesContext(DbContextOptions<UsersMessagesContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Message> Messages { get; set; }

    public DbSet<User> Users { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=UsersMessages; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.NameText).HasMaxLength(200);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Message_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Login)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(80);
            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
