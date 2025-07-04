using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Infrastructure.Data;

public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(255);
            });

            entity.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.HashedValue)
                        .HasColumnName("PasswordHash")
                        .IsRequired();
            });

            entity.Property(u => u.RefreshToken)
                .HasMaxLength(512);

            entity.Property(u => u.RefreshTokenExpiry)
                .IsRequired();
        });
        
        modelBuilder.Entity<Game>(b =>
        {
            b.HasKey(g => g.Id);
        });
    }
    }