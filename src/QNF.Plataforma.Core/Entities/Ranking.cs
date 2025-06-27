namespace QNF.Plataforma.Core.Entities;

public class Ranking : BaseEntity
{
    public Guid GrupoId { get; set; }
    public Guid JogadorId { get; set; }
    public int Jogos { get; set; } = 0;
    public int Vitorias { get; set; } = 0;
    public int Derrotas { get; set; } = 0;

    public double Aproveitamento =>
        Jogos == 0 ? 0 : Math.Round((double)Vitorias / Jogos * 100, 2);

    public Jogador Jogador { get; set; } = null!;

    public Ranking() { }

    public Ranking(Guid grupoId, Guid jogadorId)
    {
        GrupoId = grupoId;
        JogadorId = jogadorId;
    }

    public void RegistrarVitoria()
    {
        Jogos++;
        Vitorias++;
    }

    public void RegistrarDerrota()
    {
        Jogos++;
        Derrotas++;
    }
}