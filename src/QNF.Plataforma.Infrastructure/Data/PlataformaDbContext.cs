using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Infrastructure.Data;

public class PlataformaDbContext : DbContext
{
    public PlataformaDbContext(DbContextOptions<PlataformaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Jogador> Jogadores => Set<Jogador>();
    public DbSet<Grupo> Grupos => Set<Grupo>();
    public DbSet<JogadorGrupo> JogadorGrupos => Set<JogadorGrupo>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Jogador>().ToTable("Jogadores");
        modelBuilder.Entity<Grupo>().ToTable("Grupos");
        modelBuilder.Entity<JogadorGrupo>().ToTable("JogadorGrupos");
    }
}