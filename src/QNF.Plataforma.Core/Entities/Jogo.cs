using QNF.Plataforma.Core.Enums;

namespace QNF.Plataforma.Core.Entities;

public class Jogo : BaseEntity
{
    public Guid GrupoId { get; set; }
    public Guid Dupla1Id { get; set; }
    public Guid Dupla2Id { get; set; }
    public int PontuacaoDupla1 { get; set; }
    public int PontuacaoDupla2 { get; set; }
    public Guid CriadoPorJogadorId { get; set; }
    public DateTime DataHora { get; set; }
    public string? Local { get; set; }
    public JogoStatus Status { get; set; } = JogoStatus.Pendente;

    public Jogo() { }

    public Jogo(
        Guid grupoId,
        Guid dupla1Id,
        Guid dupla2Id,
        Guid criadoPorJogadorId,
        DateTime dataHora,
        string? local)
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