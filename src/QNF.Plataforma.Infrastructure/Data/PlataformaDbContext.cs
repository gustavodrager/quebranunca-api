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
    public DbSet<Dupla> Duplas => Set<Dupla>();
    public DbSet<Jogo> Jogos => Set<Jogo>();
    public DbSet<ValidacaoJogo> Validacoes => Set<ValidacaoJogo>();
    public DbSet<Ranking> Rankings => Set<Ranking>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Jogador>().ToTable("Jogadores");
        modelBuilder.Entity<Grupo>().ToTable("Grupos");
        modelBuilder.Entity<JogadorGrupo>().ToTable("JogadorGrupos");
        modelBuilder.Entity<Dupla>().ToTable("Duplas");
        modelBuilder.Entity<Jogo>().ToTable("Jogos");
        modelBuilder.Entity<ValidacaoJogo>().ToTable("Validacoes");
        modelBuilder.Entity<Ranking>().ToTable("Rankings");
    }
}