namespace QNF.Plataforma.Core.Entities;

public class Jogo : BaseEntity
{
    public Guid GrupoId { get; private set; }
    public Guid Dupla1Id { get; private set; }
    public Guid Dupla2Id { get; private set; }

    public int PontuacaoDupla1 { get; private set; }
    public int PontuacaoDupla2 { get; private set; }

    public Guid CriadoPorJogadorId { get; private set; }
    public DateTime DataHora { get; private set; }
    public string? Local { get; private set; }

    public JogoStatus Status { get; private set; } = JogoStatus.Pendente;

    private Jogo() { }

    public Jogo(Guid grupoId, Guid dupla1Id, Guid dupla2Id, Guid criadoPorJogadorId, DateTime dataHora, string? local)
    {
        if (dupla1Id == dupla2Id)
            throw new ArgumentException("As duplas devem ser diferentes.");

        GrupoId = grupoId;
        Dupla1Id = dupla1Id;
        Dupla2Id = dupla2Id;
        CriadoPorJogadorId = criadoPorJogadorId;
        DataHora = dataHora;
        Local = local;
    }

    public void RegistrarResultado(int pontuacaoDupla1, int pontuacaoDupla2)
    {
        PontuacaoDupla1 = pontuacaoDupla1;
        PontuacaoDupla2 = pontuacaoDupla2;
        Status = JogoStatus.AguardandoValidacao;
    }

    public void Aprovar() => Status = JogoStatus.Aprovado;
    public void Rejeitar() => Status = JogoStatus.Rejeitado;
}

public enum JogoStatus
{
    Pendente = 0,
    AguardandoValidacao = 1,
    Aprovado = 2,
    Rejeitado = 3
}