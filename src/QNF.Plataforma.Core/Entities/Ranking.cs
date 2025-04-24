namespace QNF.Plataforma.Core.Entities;

public class Ranking : BaseEntity
{
    public Guid GrupoId { get; private set; }
    public Guid JogadorId { get; private set; }

    public int Jogos { get; private set; } = 0;
    public int Vitorias { get; private set; } = 0;
    public int Derrotas { get; private set; } = 0;

    public double Aproveitamento =>
        Jogos == 0 ? 0 : Math.Round((double)Vitorias / Jogos * 100, 2);

    private Ranking() { }

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